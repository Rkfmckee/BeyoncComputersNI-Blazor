using BeyondComputersNi.Dal.Entities;

namespace BeyondComputersNi.Dal.Interfaces;
public interface IDataGenerator
{
    Build GenerateBuild();
    IEnumerable<Build> GenerateBuilds(int number);
    User GenerateUser();
}
