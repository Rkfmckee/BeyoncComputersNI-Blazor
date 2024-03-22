using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Dal.Contexts;

public class BaseContext : DbContext
{
    private readonly string? _connectionString;

    protected BaseContext(DbContextOptions option) : base(option) { }
    protected BaseContext(string connectionString) { _connectionString = connectionString; }

    public IQueryable<T> Get<T>() where T : class
    {
        return Set<T>().AsQueryable();
    }

    public new T Add<T>(T item) where T : class
    {
        return Set<T>().Add(item).Entity;
    }

    public void Add<T>(params T[] items) where T : class
    {
        Set<T>().AddRange(items);
    }

    public void AddAsync<T>(params T[] items) where T : class
    {
        Set<T>().AddRangeAsync(items);
    }

    public void Delete<T>(params T[] items) where T : class
    {
        Set<T>().RemoveRange(items);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        if (!string.IsNullOrWhiteSpace(_connectionString) && !optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
