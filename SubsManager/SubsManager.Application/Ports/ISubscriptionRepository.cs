using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.Ports
{
    public interface ISubscriptionRepository : IRepository<Subscription>
    {
        Task<bool> ExistsActiveForUserAndPlanAsync(Guid userId, Guid planId, CancellationToken ct = default);
        Task<Subscription?> GetWithPlanAsync(Guid subscriptionId, CancellationToken ct = default);
        Task<List<Subscription>> GetActiveByUserAsync(Guid userId, DateTimeOffset now, CancellationToken ct = default);
    }
}
