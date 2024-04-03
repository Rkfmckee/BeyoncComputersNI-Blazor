using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Shared.ViewModels.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace BeyondComputersNi.Blazor.Services.Authentication;

public class AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITokenService tokenService,
    AuthenticationStateProvider authenticationStateProvider) : BaseService(httpClientFactory, configuration), IAuthenticationService
{
    public async Task<DateTime> LoginAsync(LoginViewModel login)
    {
        var authenticationViewModel = await PostAsync<AuthenticationViewModel>("api/authentication/login", login);
        await tokenService.SetTokensAsync(authenticationViewModel);
        await authenticationStateProvider.GetAuthenticationStateAsync();

        return authenticationViewModel.AuthExpiration;
    }

    public async Task LogoutAsync()
    {
        try
        {
            await DeleteAsync("api/authentication/revoke");
        }
        catch (Exception)
        {
        }

        await tokenService.RemoveTokensAsync();
        await authenticationStateProvider.GetAuthenticationStateAsync();
    }
}
