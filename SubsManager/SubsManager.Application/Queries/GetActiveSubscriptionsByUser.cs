using Mapster;
using MediatR;
using SubsManager.Application.DTOs;
using SubsManager.Application.Ports;

namespace SubsManager.Application.Queries;

public record GetActiveSubscriptionsByUserQuery(Guid UserId) : IRequest<List<SubscriptionDto>>;

public class GetActiveSubscriptionsByUserHandler(ISubscriptionRepository subs) : IRequestHandler<GetActiveSubscriptionsByUserQuery, List<SubscriptionDto>>
{
    public async Task<List<SubscriptionDto>> Handle(GetActiveSubscriptionsByUserQuery r, CancellationToken ct)
    {
        var now = DateTimeOffset.UtcNow; // read-only ok
        var list = await subs.GetActiveByUserAsync(r.UserId, now, ct);
        return list.ConvertAll(s => s.Adapt<SubscriptionDto>());
    }
}
