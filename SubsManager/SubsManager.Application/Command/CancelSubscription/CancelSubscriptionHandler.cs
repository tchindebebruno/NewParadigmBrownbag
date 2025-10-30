using MediatR;
using Mapster;
using SubsManager.Application.Abstractions;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;

namespace SubsManager.Application.UseCases
{
    public class CancelSubscriptionHandler(ISubscriptionRepository subs, IDateTimeProvider clock, IUnitOfWork uow)
    : IRequestHandler<CancelSubscriptionCommand, SubscriptionDto>
    {
        public async Task<SubscriptionDto> Handle(CancelSubscriptionCommand r, CancellationToken ct)
        {
            var sub = await subs.GetWithPlanAsync(r.SubscriptionId, ct) ?? throw new ArgumentException("Subscription not found");
            if (r.Immediately) sub.CancelImmediately(clock.UtcNow); else sub.ScheduleCancelAtPeriodEnd();
            await subs.UpdateAsync(sub, ct);
            await uow.SaveChangesAsync(ct);
            return sub.Adapt<SubscriptionDto>();
        }
    }
}