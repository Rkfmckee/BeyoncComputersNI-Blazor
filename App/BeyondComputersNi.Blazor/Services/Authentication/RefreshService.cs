using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Shared.ViewModels.Authentication;

namespace BeyondComputersNi.Blazor.Services.Authentication;

public class RefreshService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : HttpService(httpClientFactory, configuration), IRefreshService
{
    public override string BaseUrl => "api/authentication";

    public async Task<AuthenticationViewModel?> RefreshAsync(string authTokenString, string refreshTokenString)
    {
        var refreshViewModel = new RefreshViewModel
        {
            AuthToken = authTokenString,
            RefreshToken = refreshTokenString
        };

        try
        {
            return await PostAsync<AuthenticationViewModel>("refresh", refreshViewModel);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
