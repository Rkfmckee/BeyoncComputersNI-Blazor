using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.ViewModels;
using BeyondComputersNi.Blazor.ViewModels.Authentication;

namespace BeyondComputersNi.Blazor.Services.Authentication;

public class RefreshService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : BaseService(httpClientFactory, configuration), IRefreshService
{
    public async Task<AuthenticationViewModel?> RefreshAsync(string authTokenString, string refreshTokenString)
    {
        var refreshViewModel = new RefreshViewModel
        {
            AuthToken = authTokenString,
            RefreshToken = refreshTokenString
        };

        try
        {
            return await PostAsync<AuthenticationViewModel>("api/authentication/refresh", refreshViewModel);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
