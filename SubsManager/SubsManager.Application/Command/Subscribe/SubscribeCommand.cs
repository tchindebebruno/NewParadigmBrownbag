using MediatR;
using SubsManager.Application.DTOs;

namespace SubsManager.Application.UseCases
{
    public record SubscribeCommand(Guid UserId, Guid PlanId) : IRequest<SubscriptionDto>;
}
