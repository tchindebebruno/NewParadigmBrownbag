using SubsManager.Application.Abstractions;


namespace SubsManager.Infrastructure
{
    public class UnitOfWork(SubsDbContext db) : IUnitOfWork
    { public Task<int> SaveChangesAsync(CancellationToken ct = default) => db.SaveChangesAsync(ct); }
}