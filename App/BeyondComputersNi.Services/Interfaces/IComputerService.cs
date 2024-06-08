using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.Interfaces;
public interface IComputerService
{
    Task<List<ComputerDto>> GetAllComputersAsync();
}
