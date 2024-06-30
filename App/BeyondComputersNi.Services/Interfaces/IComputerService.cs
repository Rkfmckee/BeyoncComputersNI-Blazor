using BeyondComputersNi.Services.DataTransferObjects.Build;

namespace BeyondComputersNi.Services.Interfaces;
public interface IComputerService
{
    Task<List<BuildDto>> GetAllComputersAsync();
}
