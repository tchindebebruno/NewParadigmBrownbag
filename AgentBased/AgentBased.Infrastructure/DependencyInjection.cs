using AgentBased.Application;
using AgentBased.Infrastructure.Constants;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Client;
using OpenAI;
using Scriban;

namespace AgentBased.Infrastructure
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSubsManagerTools(this IServiceCollection services, IConfiguration config)
        {
            string url = config[ConfigurationConstants.SubsmanagerMcpUrl] ?? throw new InvalidOperationException("SubsManager MCP URL is not configured.");
            var clientTransport = new HttpClientTransport(new HttpClientTransportOptions
            {
                Name = ConfigurationConstants.SubsmanagerMcpName,
                Endpoint = new Uri(url),
                ConnectionTimeout = TimeSpan.FromMinutes(1)
            });

            services.AddSingleton(async sp =>
            {
                return await McpClient.CreateAsync(clientTransport);
            });
            return services;
        }

        public static IServiceCollection AddMyAgent(this IServiceCollection services, IConfiguration config)
        {
            var openAiKey = config[ConfigurationConstants.OpenAIKeyName] ?? throw new InvalidOperationException("OpenAI API key is not configured.");
            services.AddScoped(sp =>
            {
                return new OpenAIClient(openAiKey)
                    .GetChatClient(ConfigurationConstants.GTP4oMiniName).AsChatClient();
            });

            services.AddScoped(async sp =>
            {
                var toolsTask = await sp.GetRequiredService<Task<McpClient>>();
                var prompts = sp.GetRequiredService<IPromptCollection<Template>>();
                var mcpTools = await toolsTask.ListToolsAsync();

                var agent = sp.GetRequiredService<IChatClient>()
                            .CreateAIAgent(
                                instructions: prompts.Get(PromptIdsConstants.SystemPromptId), 
                                tools: [
                                   ..mcpTools.Cast<AITool>()
                                ])
                            .AsBuilder()
                            .UseOpenTelemetry(sourceName: ConfigurationConstants.AgentTelemetrySourceName)
                            .Build();

                return agent;
            });


            return services;
        }
    }

}
