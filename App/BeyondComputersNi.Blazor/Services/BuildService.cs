using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Services;

public class BuildService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : HttpService(httpClientFactory, configuration), IBuildService
{
    public override string BaseUrl => "api/Build";

    public async Task<bool> BuildExists(int id) =>
        await GetAsync<bool>($"Exists/{id}");

    public async Task<int> CreateBuild() =>
        await PostAsync<int>("Create");

    public async Task AddComponents(BuildComponentsViewModel buildComponents) =>
        await PutAsync("Components", buildComponents);

    public async Task AddPeripherals(BuildPeripheralsViewModel buildPeripherals) =>
        await PutAsync("Peripherals", buildPeripherals);

    public async Task FinishBuild(BuildFinishViewModel buildFinish) =>
        await PutAsync("Finish", buildFinish);
}
