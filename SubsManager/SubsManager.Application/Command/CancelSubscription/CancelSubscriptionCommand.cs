using MediatR;
using SubsManager.Application.DTOs;

namespace SubsManager.Application.UseCases
{
    public record CancelSubscriptionCommand(Guid SubscriptionId, bool Immediately) : IRequest<SubscriptionDto>;
}