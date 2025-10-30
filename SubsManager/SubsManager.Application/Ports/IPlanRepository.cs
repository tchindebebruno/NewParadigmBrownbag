using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;

namespace SubsManager.Application.Ports
{
    public interface IPlanRepository : IRepository<Plan>
    {
        Task<Plan?> GetWithServiceAsync(Guid planId, CancellationToken ct = default);
    }
}
