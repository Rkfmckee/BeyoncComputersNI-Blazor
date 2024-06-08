using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Services;

public class BuildService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : HttpService(httpClientFactory, configuration), IBuildService
{
    public override string BaseUrl => "api/Build";

    public async Task<bool> BuildExistsAsync(int id) =>
        await GetAsync<bool>($"Exists/{id}");

    public async Task<BuildNumberViewModel?> GetBuildNumberAsync(int id) =>
    await GetAsync<BuildNumberViewModel?>($"Number/{id}");

    public async Task<int> CreateBuildAsync() =>
        await PostAsync<int>("Create");

    public async Task<IEnumerable<string>> GetComponentsAsync(string componentName, string? value = null) =>
        await GetAsync<IEnumerable<string>>($"Components/{componentName}?value={value}");

    public async Task AddComponentsAsync(BuildComponentsViewModel buildComponents) =>
        await PutAsync("Components", buildComponents);

    public async Task AddPeripheralsAsync(BuildPeripheralsViewModel buildPeripherals) =>
        await PutAsync("Peripherals", buildPeripherals);

    public async Task FinishBuildAsync(BuildFinishViewModel buildFinish) =>
        await PutAsync("Finish", buildFinish);
}
