using SubsManager.Domain.Enums;

namespace SubsManager.Domain.Entities
{
    public class Subscription
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid UserId { get; set; }
        public User User { get; set; } = default!;
        public required Guid PlanId { get; set; }
        public Plan Plan { get; set; } = default!;


        public SubscriptionStatus Status { get; set; } = SubscriptionStatus.Pending;
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset CurrentPeriodStart { get; set; }
        public DateTimeOffset CurrentPeriodEnd { get; set; }
        public bool AutoRenew { get; set; } = true;
        public DateTimeOffset? CanceledAt { get; set; }
        public DateTimeOffset? CancelAtPeriodEnd { get; set; }


        public void Activate(DateTimeOffset now)
        {
            Status = SubscriptionStatus.Active;
            if (StartDate == default) StartDate = now;
            if (CurrentPeriodStart == default) CurrentPeriodStart = now;
        }


        public void ScheduleCancelAtPeriodEnd() => CancelAtPeriodEnd = CurrentPeriodEnd;


        public void CancelImmediately(DateTimeOffset now)
        {
            Status = SubscriptionStatus.Canceled;
            AutoRenew = false;
            CanceledAt = now;
            CancelAtPeriodEnd = now;
        }

        public void AdvancePeriod()
        {
            CurrentPeriodStart = CurrentPeriodEnd;
            var months = (int)Plan.Period;
            CurrentPeriodEnd = CurrentPeriodStart.AddMonths(months);
        }
    }
}