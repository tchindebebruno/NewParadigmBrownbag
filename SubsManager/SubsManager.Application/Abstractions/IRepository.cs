namespace SubsManager.Application.Abstractions
{
    public interface IRepository<T> : IReadRepository<T>
    {
        Task AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);
    }
}
