using BeyondComputersNi.Dal.Entities;

namespace BeyondComputersNi.Dal.Interfaces;

public interface IRepository<T> where T : Entity
{
    IQueryable<T> Get();
    void Add(params T[] items);
    Task AddAsync(params T[] items);
    void Delete(params T[] items);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
