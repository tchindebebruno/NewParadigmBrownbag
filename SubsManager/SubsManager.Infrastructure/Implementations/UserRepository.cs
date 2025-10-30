using Microsoft.EntityFrameworkCore;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;

namespace SubsManager.Infrastructure.Repositories
{
    public class UserRepository(SubsDbContext db) : RepositoryBase<User>(db), IUserRepository
    {
        public Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        => _db.Users.FirstOrDefaultAsync(u => u.Email == email, ct);

        public override async Task<IEnumerable<User>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Users.ToListAsync(ct);
        }
    }
}
