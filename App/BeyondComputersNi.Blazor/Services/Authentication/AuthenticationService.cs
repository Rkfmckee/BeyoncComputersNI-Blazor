using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.ViewModels;
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
        await DeleteAsync("api/authentication/revoke");
        await tokenService.RemoveTokensAsync();
        await authenticationStateProvider.GetAuthenticationStateAsync();
    }
}
