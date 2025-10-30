using Microsoft.EntityFrameworkCore;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;

namespace SubsManager.Infrastructure.Repositories
{
    public class ServiceRepository(SubsDbContext db) : RepositoryBase<Service>(db), IServiceRepository
    {
        public override async Task<IEnumerable<Service>> GetAllAsync(CancellationToken ct = default)
        {
            return await _db.Services.ToListAsync(ct);
        }
    }
}
