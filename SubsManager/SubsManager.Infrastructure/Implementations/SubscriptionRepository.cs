using Microsoft.EntityFrameworkCore;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;
using SubsManager.Domain.Enums;

namespace SubsManager.Infrastructure.Repositories
{
    public class SubscriptionRepository(SubsDbContext db) : RepositoryBase<Subscription>(db), ISubscriptionRepository
    {
        public Task<bool> ExistsActiveForUserAndPlanAsync(Guid userId, Guid planId, CancellationToken ct = default)
        => _db.Subscriptions.AnyAsync(s => s.UserId == userId && s.PlanId == planId && s.Status != SubscriptionStatus.Canceled, ct);


        public Task<Subscription?> GetWithPlanAsync(Guid subscriptionId, CancellationToken ct = default)
        => _db.Subscriptions.Include(s => s.Plan).FirstOrDefaultAsync(s => s.Id == subscriptionId, ct);


        public async Task<List<Subscription>> GetActiveByUserAsync(Guid userId, DateTimeOffset now, CancellationToken ct = default)
        => await _db.Subscriptions.Include(s => s.Plan).ThenInclude(p => p.Service)
        .Where(s => s.UserId == userId && s.Status != SubscriptionStatus.Canceled && s.CurrentPeriodEnd > now)
        .ToListAsync(ct);
    }
}
