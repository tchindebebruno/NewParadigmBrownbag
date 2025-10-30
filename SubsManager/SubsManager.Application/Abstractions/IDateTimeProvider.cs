namespace SubsManager.Application.Abstractions
{
    public interface IDateTimeProvider { DateTimeOffset UtcNow { get; } }
}