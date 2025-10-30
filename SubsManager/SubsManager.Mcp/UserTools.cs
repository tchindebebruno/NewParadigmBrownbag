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
    [McpServerTool, Description("Gets all users")]
    public static async Task<IEnumerable<UserDto>> GetUsers(IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(new GetUsersQuery());
    }

    [McpServerTool, Description("Gets a user by Id")]
    public static async Task<UserDto?> GetUserById([Description("The Id of the user")] Guid id, IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(new GetUserQuery(id));
    }

    [McpServerTool, Description("Creates a new user")]
    public static async Task<UserDto> CreateUser([Description("The user to create")] CreateUserCommand cmd, IServiceProvider serviceProvider)
    {
        var sender = serviceProvider.GetRequiredService<ISender>();
        return await sender.Send(cmd);
    }
}
