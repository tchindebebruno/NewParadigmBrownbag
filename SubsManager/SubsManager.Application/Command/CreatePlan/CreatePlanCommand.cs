using MediatR;
using SubsManager.Application.DTOs;

namespace SubsManager.Application.UseCases
{
    public record CreatePlanCommand(Guid ServiceId, string Name, decimal Price, string Currency, int Period, int TrialDays) : IRequest<PlanDto>;
}
