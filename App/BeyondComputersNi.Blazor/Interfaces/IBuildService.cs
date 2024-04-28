using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Interfaces;

public interface IBuildService
{
    string BaseUrl { get; }

    Task<bool> BuildExists(int id);
    Task<BuildNumberViewModel?> GetBuildNumber(int id);
    Task<int> CreateBuild();
    Task AddComponents(BuildComponentsViewModel buildComponents);
    Task AddPeripherals(BuildPeripheralsViewModel buildPeripherals);
    Task FinishBuild(BuildFinishViewModel buildFinish);
}