using Mapster;
using MediatR;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;

namespace SubsManager.Application.Queries
{
    public record GetUsersQuery() : IRequest<List<UserDto>>;
    
    public class GetUsersHandler(IUserRepository users) : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken ct)
        {
            var all = await users.GetAllAsync(ct);
            return all.Adapt<List<UserDto>>();
        }
    }
}
