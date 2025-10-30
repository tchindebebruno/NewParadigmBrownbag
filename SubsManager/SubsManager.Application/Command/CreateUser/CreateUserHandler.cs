using MediatR;
using Mapster;
using SubsManager.Application.Abstractions;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.UseCases
{
    public class CreateUserHandler(IUserRepository users, IUnitOfWork uow) : IRequestHandler<CreateUserCommand, UserDto>
    {
        public async Task<UserDto> Handle(CreateUserCommand r, CancellationToken ct)
        {
            var user = new User { Email = r.Email, FullName = r.FullName };
            await users.AddAsync(user, ct);
            await uow.SaveChangesAsync(ct);
            return user.Adapt<UserDto>();
        }
    }
}
