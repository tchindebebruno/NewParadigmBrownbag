using Microsoft.EntityFrameworkCore;
using SubsManager.Application.Abstractions;
using SubsManager.Domain.Entities;

namespace SubsManager.Infrastructure.Repositories
{
    public abstract class RepositoryBase<T>(SubsDbContext db) : IRepository<T> where T : class
    {
        protected readonly SubsDbContext _db = db;
        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) => await _db.Set<T>().FindAsync([id], ct);
        public virtual Task AddAsync(T entity, CancellationToken ct = default) { _db.Set<T>().Add(entity); return Task.CompletedTask; }
        public virtual Task UpdateAsync(T entity, CancellationToken ct = default) { _db.Set<T>().Update(entity); return Task.CompletedTask; }
        public virtual Task DeleteAsync(T entity, CancellationToken ct = default) { _db.Set<T>().Remove(entity); return Task.CompletedTask; }
        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct = default) { return await _db.Set<T>().ToListAsync(ct); }
    }
}
