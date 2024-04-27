using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels;

namespace BeyondComputersNi.Blazor.Services;

public class ComputerService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : HttpService(httpClientFactory, configuration), IComputerService
{
    public override string BaseUrl => "api/computer";

    public Task<List<ComputerViewModel>> GetAllComputers()
    {
        return GetAsync<List<ComputerViewModel>>("");
    }
}
