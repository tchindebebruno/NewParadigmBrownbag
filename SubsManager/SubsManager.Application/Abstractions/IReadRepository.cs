namespace SubsManager.Application.Abstractions
{
    public interface IReadRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
