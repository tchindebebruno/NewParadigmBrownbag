using Microsoft.EntityFrameworkCore;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;

namespace SubsManager.Infrastructure.Repositories
{
    public class PlanRepository(SubsDbContext db) : RepositoryBase<Plan>(db), IPlanRepository
    {
        public Task<Plan?> GetWithServiceAsync(Guid planId, CancellationToken ct = default)
        => _db.Plans.Include(p => p.Service).FirstOrDefaultAsync(p => p.Id == planId, ct);
    }
}
