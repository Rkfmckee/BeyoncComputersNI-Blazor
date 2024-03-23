using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using Bogus;

namespace BeyondComputersNi.Dal.Database;

public class DataGenerator : IDataGenerator
{
    private const int seed = 123;

    private Faker<Computer> fakeComputer;

    public DataGenerator()
    {
        var random = new Random(seed);
        Randomizer.Seed = random;

        fakeComputer = new Faker<Computer>()
            .RuleFor(c => c.Identifier,  (f, c) => $"{f.Name.FirstName()}'s computer")
            .RuleFor(c => c.Motherboard, (f, c) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.AlphaNumeric(5)}")
            .RuleFor(c => c.CPU,         (f, c) => $"{f.Random.Word()} Core {f.Random.Char('a', 'z')}{f.Random.Number(100)}")
            .RuleFor(c => c.CPUCooler,   (f, c) => $"{c.CPU} Cooler")
            .RuleFor(c => c.Memory,      (f, c) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(100)}GB DDR{f.Random.Number(100)}")
            .RuleFor(c => c.Storage,     (f, c) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(100)}TB HDD")
            .RuleFor(c => c.GPU,         (f, c) => $"{f.Random.Word()} {f.Random.Char('A', 'Z')}TX {f.Random.Number(9)}0{f.Random.Number(9)}0")
            .RuleFor(c => c.PSU,         (f, c) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(1000)}W")
            .RuleFor(c => c.Case,        (f, c) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Music.Genre()}");
    }

    public Computer GenerateComputer()
    {
        return fakeComputer.Generate();
    }

    public IEnumerable<Computer> GenerateComputers(int number)
    {
        return fakeComputer.GenerateForever().Take(number);
    }
}
