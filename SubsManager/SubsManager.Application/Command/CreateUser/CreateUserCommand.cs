using MediatR;
using SubsManager.Application.DTOs;

namespace SubsManager.Application.UseCases
{
    public record CreateUserCommand(string Email, string? FullName) : IRequest<UserDto>;
}
