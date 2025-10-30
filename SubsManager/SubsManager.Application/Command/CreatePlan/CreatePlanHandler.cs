using MediatR;
using Mapster;
using SubsManager.Application.Abstractions;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;
using SubsManager.Domain.Entities;
using SubsManager.Domain.Enums;

namespace SubsManager.Application.UseCases
{
    public class CreatePlanHandler(IPlanRepository plans, IUnitOfWork uow) : IRequestHandler<CreatePlanCommand, PlanDto>
    {
        public async Task<PlanDto> Handle(CreatePlanCommand r, CancellationToken ct)
        {
            var plan = new Plan
            {
                ServiceId = r.ServiceId,
                Name = r.Name,
                Price = r.Price,
                Currency = string.IsNullOrWhiteSpace(r.Currency) ? "USD" : r.Currency,
                Period = r.Period == 12 ? BillingPeriod.Year : BillingPeriod.Month,
                TrialDays = r.TrialDays, 
                IsActive = true
            };
            await plans.AddAsync(plan, ct);
            await uow.SaveChangesAsync(ct);
            return plan.Adapt<PlanDto>();
        }
    }
}
