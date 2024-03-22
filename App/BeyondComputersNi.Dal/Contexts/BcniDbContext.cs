using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Dal.Contexts;

public class BcniDbContext : BaseContext, IBcniDbContext
{
    public BcniDbContext(DbContextOptions option) : base(option) { }
    public BcniDbContext(string connectionString) : base(connectionString) { }

    public DbSet<Computer> Computers { get; set; }
}
