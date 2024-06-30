using BeyondComputersNi.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Dal.Database;

public class BcniDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Build> Builds { get; set; }
    public DbSet<User> Users { get; set; }
}
