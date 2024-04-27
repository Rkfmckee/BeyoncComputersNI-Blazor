using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;

namespace BeyondComputersNi.Blazor.Services;

public class BuildService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : HttpService(httpClientFactory, configuration), IBuildService
{
    public override string BaseUrl => "api/build";

    public async Task<int> CreateBuild() =>
        await PostAsync<int>("create");

    public async Task AddComponents(BuildComponentsViewModel buildComponents) =>
        await PutAsync("components", buildComponents);
}
