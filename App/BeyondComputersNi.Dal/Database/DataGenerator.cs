using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using Bogus;
using System.Drawing;

namespace BeyondComputersNi.Dal.Database;

public class DataGenerator : IDataGenerator
{
    private const int seed = 123;

    private Faker<Build> fakeBuild;
    private Faker<User> fakeUser;

    public DataGenerator()
    {
        var random = new Random(seed);
        Randomizer.Seed = random;

        var allColours = (KnownColor[])Enum.GetValues(typeof(KnownColor));

        fakeBuild = new Faker<Build>()
            .RuleFor(b => b.Identifier, (f, b) => $"{f.Name.FirstName()}'s computer")
            .RuleFor(b => b.BuildNumber, (f, b) => $"BLD/20{f.Random.Number(99)}/{f.Random.Number(11) + 1}/{f.Random.Number(30) + 1}/{f.Random.Number(100)}")

            .RuleFor(b => b.Motherboard, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.AlphaNumeric(5)}")
            .RuleFor(b => b.CPU, (f, b) => $"{f.Random.Word()} Core {f.Random.Char('a', 'z')}{f.Random.Number(100)}")
            .RuleFor(b => b.CPUCooler, (f, b) => $"{b.CPU} Cooler")
            .RuleFor(b => b.Memory, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(100)}GB DDR{f.Random.Number(100)}")
            .RuleFor(b => b.Storage, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(100)}TB HDD")
            .RuleFor(b => b.GPU, (f, b) => $"{f.Random.Word()} {f.Random.Char('A', 'Z')}TX {f.Random.Number(9)}0{f.Random.Number(9)}0")
            .RuleFor(b => b.PSU, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(1000)}W")

            .RuleFor(b => b.Case, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} Case {allColours[random.Next(allColours.Length)]}")
            .RuleFor(b => b.Keyboard, (f, b) => $"{f.Random.Word()} {f.Random.Char('a', 'z')}{f.Random.Number(100)} RGB")
            .RuleFor(b => b.Mouse, (f, b) => $"{f.Random.Word()} {f.Random.Char('a', 'z')}{f.Random.Number(100)} RGB")
            .RuleFor(b => b.Monitor, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(9999)}x{f.Random.Number(9999)} {f.Random.Number(1000)}Hz")
            .RuleFor(b => b.Speakers, (f, b) => $"{f.Random.Word()} {f.Commerce.ProductName()} {f.Random.Number(10)}.0 Surround-sound");

        fakeUser = new Faker<User>()
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
            .RuleFor(u => u.Name, (f, u) => f.Name.FullName());
    }

    public Build GenerateBuild() => fakeBuild.Generate();
    public IEnumerable<Build> GenerateBuilds(int number) => fakeBuild.GenerateForever().Take(number);

    public User GenerateUser() => fakeUser.Generate();
}
