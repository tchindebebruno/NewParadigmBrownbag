using AgentBased.Application;
using AgentBased.Infrastructure.Constants;
using Microsoft.Agents.AI;
using AgentBased.Application.DataRequests;
using Scriban;
using System.Text.Json;
using ModelContextProtocol.Client;

namespace AgentBased.Api.Endpoints
{
    public static class UsersEndpoints
    {
        public static void MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/users");

            group.MapGet("/", async (Task<AIAgent> agentFactory, IPromptCollection < Template > prompt, int? page=0,int? pageSize=10) => {
                var agent = await agentFactory;

                var completionStream = await agent.RunAsync(prompt.Get(PromptIdsConstants.GetUsersPromptId, new() { { "page", page }, { "pageSize", pageSize } }));
                var json = JsonSerializer.Deserialize<JsonElement>(completionStream.Text);

                return Results.Ok(json);
            });

            group.MapPost("/", async (Task<AIAgent> agentFactory, IPromptCollection<Template> prompt,CreateUser user) =>
            {
                var agent = await agentFactory;
                var promptText = prompt.Get(PromptIdsConstants.CreateUserPromptId, new() { { "fullName", user.FullName }, { "email", user.Email } });
               
                var completionStream = await agent.RunAsync(promptText);
                var json = JsonSerializer.Deserialize<JsonElement>(completionStream.Text);

                return Results.Ok(json);
            });

            group.MapGet("/{id}", async (Task<AIAgent> agentFactory, IPromptCollection < Template > prompts, Guid id) =>
            {
                var agent = await agentFactory;
                var prompt = prompts.Get(PromptIdsConstants.GetUserByIdPromptId, new() { { "id", id } });
                var completionText = await agent.RunAsync(prompt);
                var json = JsonSerializer.Deserialize<JsonElement>(completionText.Text);
                return Results.Ok(json);
            });

            group.Map("/tools/list", async (Task<McpClient> mcpClientTask) =>
            {
                var mcpClient = await mcpClientTask;
                var mcpTools = await mcpClient.ListToolsAsync();
                return Results.Ok(mcpTools);
            });

        }
    }
}
