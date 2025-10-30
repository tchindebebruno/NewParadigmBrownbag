using SubsManager.Domain.Enums;

namespace SubsManager.Domain.Entities
{
    public class Plan
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required Guid ServiceId { get; set; }
        public Service Service { get; set; } = default!;


        public required string Name { get; set; }
        public required decimal Price { get; set; }
        public required string Currency { get; set; } = "USD";
        public required BillingPeriod Period { get; set; } = BillingPeriod.Month;
        public int TrialDays { get; set; } = 0;
        public bool IsActive { get; set; } = true;


        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}