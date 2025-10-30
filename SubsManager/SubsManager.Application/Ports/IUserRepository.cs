using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.Ports
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
