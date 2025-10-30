namespace AgentBased.Application
{
    public interface IPromptCollection<T>
    {
        string Get(string name, Dictionary<string, dynamic?>? variables = null);
        Dictionary<string, T> GetAllPrompts();
    }
}
