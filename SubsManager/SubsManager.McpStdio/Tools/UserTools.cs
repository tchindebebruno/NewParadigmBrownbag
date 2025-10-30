using System.ComponentModel;
using MediatR;
using SubsManager.Application.DTOs;
using SubsManager.Application.Queries;
using SubsManager.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using ModelContextProtocol.Server;

[McpServerToolType]
public static class UserTools
{
    [McpServerTool, Description("Retrieves all users and returns them as an IEnumerable of UserDto. The query is handled by the application layer and returns all available user records.")]
    public static async Task<IEnumerable<UserDto>> GetUsers(IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(new GetUsersQuery());
    }

    [McpServerTool, Description("Retrieves a single user by GUID. Returns the matching UserDto if found; otherwise null. The provided 'id' should be a valid GUID. Validation and data retrieval are performed by the application layer.")]
    public static async Task<UserDto?> GetUserById([Description("The Id of the user")] Guid id, IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(new GetUserQuery(id));
    }

    [McpServerTool, Description("Creates a new user from the provided email and fullName. Returns the created UserDto. " +
                               "The command should include the user properties to persist; validation and persistence are performed by the application layer.")]
    public static async Task<UserDto> CreateUser([Description("The valid email adresse here")] string email, [Description("Valid fullName here")] string fullName, IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(new CreateUserCommand(email, fullName));
    }
}
