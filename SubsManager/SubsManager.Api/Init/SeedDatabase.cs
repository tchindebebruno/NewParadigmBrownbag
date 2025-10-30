using Microsoft.EntityFrameworkCore;
using SubsManager.Infrastructure;

public class SeedDatabase {
    public static async Task RunAsync(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SubsDbContext>();
            await db.Database.EnsureCreatedAsync();

            if (!db.Services.Any())
            {
                var s = new SubsManager.Domain.Entities.Service { Name = "Email Pro", Description = "Email pro" };
                db.Services.Add(s);
                await db.SaveChangesAsync();

                db.Plans.AddRange(
                    new SubsManager.Domain.Entities.Plan { ServiceId = s.Id, Name = "Basic", Price = 5m, Currency = "USD", Period = SubsManager.Domain.Enums.BillingPeriod.Month, TrialDays = 7 },
                    new SubsManager.Domain.Entities.Plan { ServiceId = s.Id, Name = "Pro", Price = 9m, Currency = "USD", Period = SubsManager.Domain.Enums.BillingPeriod.Month }
                );
                await db.SaveChangesAsync();
            }

            if (!db.Users.Any())
            {
                db.Users.Add(new SubsManager.Domain.Entities.User { FullName = "John Doe", Email = "john.doe@example.com" });
                db.Users.Add(new SubsManager.Domain.Entities.User { FullName = "Jane Smith", Email = "jane.smith@example.com" });
                db.Users.Add(new SubsManager.Domain.Entities.User { FullName = "Alice Johnson", Email = "alice.johnson@example.com" });
                await db.SaveChangesAsync();
            }

            if (!db.Subscriptions.Any())
            {
                var user = await db.Users.FirstAsync();
                var plan = await db.Plans.FirstAsync();

                db.Subscriptions.Add(new SubsManager.Domain.Entities.Subscription
                {
                    UserId = user.Id,
                    PlanId = plan.Id,
                    StartDate = DateTime.UtcNow,
                    AutoRenew = true,
                    Status = SubsManager.Domain.Enums.SubscriptionStatus.Active,
                    CurrentPeriodEnd = DateTime.UtcNow.AddMonths(1),
                    Plan = plan,
                    User = user,
                    CurrentPeriodStart = DateTime.UtcNow,
                });
                await db.SaveChangesAsync();
            }
        }
    }
}