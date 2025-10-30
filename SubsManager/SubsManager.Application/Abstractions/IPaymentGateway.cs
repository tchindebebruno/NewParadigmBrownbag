namespace SubsManager.Application.Abstractions
{
    public interface IPaymentGateway
    {
        Task<bool> AuthorizeAsync(Guid userId, Guid planId, decimal amount, string currency, CancellationToken ct);
    }
}
