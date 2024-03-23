using BeyondComputersNi.Dal.Entities;

namespace BeyondComputersNi.Dal.Interfaces;
public interface IDataGenerator
{
    Computer GenerateComputer();
    IEnumerable<Computer> GenerateComputers(int number);
}
