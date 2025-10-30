using MediatR;
using Mapster;
using SubsManager.Application.Ports;
using SubsManager.Application.DTOs;
using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;
using SubsManager.Domain.Enums;

namespace SubsManager.Application.UseCases
{
    public class SubscribeHandler(IUserRepository users, IPlanRepository plans, ISubscriptionRepository subs, IDateTimeProvider clock, IPaymentGateway payments, IUnitOfWork uow)
    : IRequestHandler<SubscribeCommand, SubscriptionDto>
    {
        public async Task<SubscriptionDto> Handle(SubscribeCommand r, CancellationToken ct)
        {
            var user = await users.GetByIdAsync(r.UserId, ct) ?? throw new ArgumentException("Invalid user");
            var plan = await plans.GetWithServiceAsync(r.PlanId, ct) ?? throw new ArgumentException("Invalid plan");


            if (await subs.ExistsActiveForUserAndPlanAsync(user.Id, plan.Id, ct))
                throw new InvalidOperationException("Subscription already exists for this plan");


            var now = clock.UtcNow;
            var sub = new Subscription
            {
                UserId = user.Id,
                PlanId = plan.Id,
                Status = SubscriptionStatus.Active,
                StartDate = now,
                CurrentPeriodStart = now,
                CurrentPeriodEnd = plan.TrialDays > 0 ? now.AddDays(plan.TrialDays) : now.AddMonths((int)plan.Period),
                AutoRenew = true
            };


            if (plan.TrialDays == 0)
            {
                var ok = await payments.AuthorizeAsync(user.Id, plan.Id, plan.Price, plan.Currency, ct);
                if (!ok) sub.Status = SubscriptionStatus.PastDue;
            }


            await subs.AddAsync(sub, ct);
            await uow.SaveChangesAsync(ct);
            return sub.Adapt<SubscriptionDto>();
        }
    }
}
