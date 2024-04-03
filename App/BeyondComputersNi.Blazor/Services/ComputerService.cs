using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Shared.ViewModels;

namespace BeyondComputersNi.Blazor.Services;

public class ComputerService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : BaseService(httpClientFactory, configuration), IComputerService
{
    public Task<List<ComputerViewModel>> GetAllComputers()
    {
        return GetAsync<List<ComputerViewModel>>("api/computer");
    }
}
