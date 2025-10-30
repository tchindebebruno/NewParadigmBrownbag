using Mapster;
using MediatR;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;

namespace SubsManager.Application.Queries;

public record GetUserQuery(Guid Id) : IRequest<UserDto?>;

public class GetUserHandler(IUserRepository users) : IRequestHandler<GetUserQuery, UserDto?>
{
    public async Task<UserDto?> Handle(GetUserQuery r, CancellationToken ct)
    {
        var u = await users.GetByIdAsync(r.Id, ct);
        return u?.Adapt<UserDto>();
    }
}
