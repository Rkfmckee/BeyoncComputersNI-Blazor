using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace BeyondComputersNi.Dal.Database;

public static class Seed
{
    private static IServiceScope? serviceScope;
    private static BcniDbContext? dbContext;
    private static IDataGenerator? dataGenerator;

    private const string defaultPassword = "Pa$$w0rd";

    public static WebApplication SeedData(this WebApplication app)
    {
        using (serviceScope = app.Services.CreateScope())
        {
            using (dbContext = serviceScope.ServiceProvider.GetRequiredService<BcniDbContext>())
            {
                dataGenerator = serviceScope.ServiceProvider.GetRequiredService<IDataGenerator>();

                dbContext.Database.EnsureCreated();

                SeedComputers();
                SeedUsers();

                dbContext.SaveChanges();
            }
        }

        return app;
    }

    private static void SeedComputers()
    {
        if (EntityHasValues<Computer>()) return;

        var computers = dataGenerator!.GenerateComputers(10);

        dbContext!.Set<Computer>().AddRange(computers);
    }

    private static void SeedUsers()
    {
        if (EntityHasValues<User>()) return;

        var users = new List<User>
        {
            new User
            {
                Email = "admin@bcni.local",
                PasswordHash = BC.HashPassword(defaultPassword),
                Name = "Admin"
            },
            new User
            {
                Email = "user@bcni.local",
                PasswordHash = BC.HashPassword(defaultPassword),
                Name = dataGenerator!.GenerateUser().Name
            }
        };

        dbContext!.Set<User>().AddRange(users);
    }

    private static bool EntityHasValues<T>() where T : Entity
    {
        var existingItems = dbContext!.Set<T>().FirstOrDefault();

        return existingItems is not null;
    }
}
