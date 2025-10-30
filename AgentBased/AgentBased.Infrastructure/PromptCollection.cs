
using Scriban;

namespace AgentBased.Application
{

    public class PromptCollection: IPromptCollection<Template>
    {
        private readonly Dictionary<string, Template> _prompts = new();

        public PromptCollection(string basePath = "Prompts")
        {
            LoadAll(basePath);
        }

        public string Get(string name, Dictionary<string, dynamic?>? variables = null)
        {
            if (!_prompts.TryGetValue(name, out var template))
                throw new KeyNotFoundException($"Prompt '{name}' introuvable.");

            if (variables is null || variables.Count == 0)
                return template.Render();

            return template.Render(variables);
        }

        public Dictionary<string, Template> GetAllPrompts()
        {
            return _prompts;
        }

        private void LoadAll(string baseDir)
        {
            baseDir = Path.Combine(Directory.GetCurrentDirectory(), baseDir);
            if (!Directory.Exists(baseDir))
                throw new DirectoryNotFoundException($"Le dossier '{baseDir}' est introuvable.");

            var files = Directory.EnumerateFiles(baseDir, "*.md", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var key = Path.GetRelativePath(baseDir, file)
                              .Replace("\\", "/")
                              .Replace(".md", "", StringComparison.OrdinalIgnoreCase);
                var raw = File.ReadAllText(file, System.Text.Encoding.UTF8);
                _prompts[key] = Template.Parse(raw);
            }
        }
    }

}
