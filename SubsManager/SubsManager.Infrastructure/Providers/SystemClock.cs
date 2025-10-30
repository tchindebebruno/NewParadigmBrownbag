using SubsManager.Application.Abstractions;

namespace SubsManager.Infrastructure.Providers
{
    public class SystemClock : IDateTimeProvider { public DateTimeOffset UtcNow => DateTimeOffset.UtcNow; }
}
