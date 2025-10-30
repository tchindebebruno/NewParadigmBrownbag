using SubsManager.Application.Abstractions;

namespace SubsManager.Infrastructure
{
    public class FakePaymentGateway : IPaymentGateway
    {
        public Task<bool> AuthorizeAsync(Guid userId, Guid planId, decimal amount, string currency, CancellationToken ct)
        => Task.FromResult(amount < 1000m);
    }
}