using Microsoft.EntityFrameworkCore;
using SubsManager.Domain.Entities;

namespace SubsManager.Infrastructure
{


    public class SubsDbContext(DbContextOptions<SubsDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Service> Services => Set<Service>();
        public DbSet<Plan> Plans => Set<Plan>();
        public DbSet<Subscription> Subscriptions => Set<Subscription>();


        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<User>(e =>
            {
                e.HasIndex(x => x.Email).IsUnique();
                e.Property(x => x.Email).IsRequired().HasMaxLength(256);
            });
            b.Entity<Service>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(128);
                e.HasMany(x => x.Plans).WithOne(p => p.Service).HasForeignKey(p => p.ServiceId);
            });
            b.Entity<Plan>(e =>
            {
                e.Property(x => x.Name).IsRequired().HasMaxLength(64);
                e.Property(x => x.Price).HasPrecision(12, 2);
                e.HasIndex(x => new { x.ServiceId, x.Name, x.Period }).IsUnique();
            });
            b.Entity<Subscription>(e =>
            {
                e.HasIndex(x => new { x.UserId, x.PlanId }).IsUnique();
                e.HasOne(s => s.User).WithMany(u => u.Subscriptions).HasForeignKey(s => s.UserId);
                e.HasOne(s => s.Plan).WithMany(p => p.Subscriptions).HasForeignKey(s => s.PlanId);
            });
        }
    }
}
