using BeyondComputersNi.Services.DataTransferObjects.Build;

namespace BeyondComputersNi.Services.Interfaces;

public interface IBuildService
{
    Task<bool> BuildExistsAsync(int id);
    Task<int?> CreateBuildAsync();
    Task<IEnumerable<string>> GetComponentsAsync(string componentName, string? value = null);
    Task<bool> AddComponentsAsync(BuildComponentsDto buildComponents);
    Task<bool> AddPeripheralsAsync(BuildPeripheralsDto buildPeripherals);
    Task<bool> FinishBuildAsync(BuildFinishDto buildFinish);
    Task<string?> GetBuildNumberAsync(int id);
}