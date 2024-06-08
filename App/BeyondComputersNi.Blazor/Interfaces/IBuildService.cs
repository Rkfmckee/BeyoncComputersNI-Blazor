using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Interfaces;

public interface IBuildService
{
    string BaseUrl { get; }

    Task<bool> BuildExistsAsync(int id);
    Task<BuildNumberViewModel?> GetBuildNumberAsync(int id);
    Task<int> CreateBuildAsync();
    Task<IEnumerable<string>> GetComponentsAsync(string componentName, string? value = null);
    Task AddComponentsAsync(BuildComponentsViewModel buildComponents);
    Task AddPeripheralsAsync(BuildPeripheralsViewModel buildPeripherals);
    Task FinishBuildAsync(BuildFinishViewModel buildFinish);
}