using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;

namespace BeyondComputersNi.Dal.Database;

public class Repository<T>(BcniDbContext dbContext) : IRepository<T> where T : Entity
{
    public IQueryable<T> Get()
    {
        return dbContext.Set<T>().AsQueryable();
    }

    public void Add(params T[] items)
    {
        dbContext.Set<T>().AddRange(items);
    }

    public Task AddAsync(params T[] items)
    {
        return dbContext.Set<T>().AddRangeAsync(items);
    }

    public void Delete(params T[] items)
    {
        dbContext.Set<T>().RemoveRange(items);
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync();
    }
}
