using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;

namespace BeyondComputersNi.Dal.Database;

public static class Seed
{
    private static IServiceScope? serviceScope;
    private static BcniDbContext? dbContext;
    private static IDataGenerator? dataGenerator;

    public static WebApplication SeedData(this WebApplication app)
    {
        using (serviceScope = app.Services.CreateScope())
        {
            using (dbContext = serviceScope.ServiceProvider.GetRequiredService<BcniDbContext>())
            {
                dataGenerator = serviceScope.ServiceProvider.GetRequiredService<IDataGenerator>();

                dbContext.Database.EnsureCreated();

                SeedComputers();

                dbContext.SaveChanges();
            }
        }

        return app;
    }

    private static void SeedComputers()
    {
        if (EntityHasValues<Computer>()) return;

        var computers = dataGenerator!.GenerateComputers(10);

        dbContext!.Set<Computer>().AddRange(dataGenerator!.GenerateComputers(10));
    }

    private static bool EntityHasValues<T>() where T : Entity
    {
        var existingItems = dbContext!.Set<T>().FirstOrDefault();

        return existingItems is not null;
    }
}
