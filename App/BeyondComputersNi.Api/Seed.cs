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

                SeedBuilds();
                SeedUsers();

                dbContext.SaveChanges();
            }
        }

        return app;
    }

    private static void SeedBuilds()
    {
        if (CantAddItems<Build>()) return;

        var builds = dataGenerator!.GenerateBuilds(10);

        dbContext!.AddRange(builds);
    }

    private static void SeedUsers()
    {
        if (CantAddItems<User>()) return;

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

        dbContext!.AddRange(users);
    }

    private static bool CantAddItems<T>() where T : Entity
    {
        return dbContext is null ||
            dataGenerator is null ||
            dbContext.Set<T>().Any();
    }
}
