using BeyondComputersNi.Services.DataTransferObjects.Build;

namespace BeyondComputersNi.Services.Interfaces;

public interface IBuildService
{
    Task<bool> BuildExists(int id);
    Task<int?> CreateBuild();
    Task<bool> AddComponents(BuildComponentsDto buildComponents);
    Task<bool> AddPeripherals(BuildPeripheralsDto buildPeripherals);
    Task<bool> FinishBuild(BuildFinishDto buildFinish);
}