namespace SubsManager.Application.DTOs
{
    public record SubscriptionDto(
        Guid Id,
        Guid UserId,
        Guid PlanId,
        string Status,
        DateTimeOffset StartDate,
        DateTimeOffset CurrentPeriodStart,
        DateTimeOffset CurrentPeriodEnd,
        bool AutoRenew,
        DateTimeOffset? CanceledAt,
        DateTimeOffset? CancelAtPeriodEnd,
        string PlanName,
        string ServiceName
    );
}
