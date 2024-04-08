using BeyondComputersNi.Blazor.Interfaces;

namespace BeyondComputersNi.Blazor.Services;

public class BuildService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    : HttpService(httpClientFactory, configuration), IBuildService
{
    public override string BaseUrl => "api/build";

    public async Task<int> CreateBuild()
    {
        return await PostAsync<int>("create");
    }
}
