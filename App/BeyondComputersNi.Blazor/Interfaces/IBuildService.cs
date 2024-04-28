using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Interfaces;

public interface IBuildService
{
    string BaseUrl { get; }

    Task<int> CreateBuild();
    Task AddComponents(BuildComponentsViewModel buildComponents);
    Task AddPeripherals(BuildPeripheralsViewModel buildPeripherals);
    Task<bool> BuildExists(int id);
    Task FinishBuild(BuildFinishViewModel buildFinish);
}