namespace SubsManager.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }


        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}