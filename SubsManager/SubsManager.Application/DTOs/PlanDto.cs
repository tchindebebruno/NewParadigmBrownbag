namespace SubsManager.Application.DTOs
{
    public record PlanDto(
        Guid Id,
        Guid ServiceId,
        string Name,
        decimal Price,
        string Currency,
        int Period,
        int TrialDays,
        bool IsActive
    );
}
