using BeyondComputersNi.Dal.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Dal.Database;

public class BcniDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Computer> Computers { get; set; }
}
