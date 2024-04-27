using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.ViewModels.Authentication;
using Microsoft.AspNetCore.Components.Authorization;

namespace BeyondComputersNi.Blazor.Services.Authentication;

public class AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration, ITokenService tokenService,
    AuthenticationStateProvider authenticationStateProvider) : HttpService(httpClientFactory, configuration), IAuthenticationService
{
    public override string BaseUrl => "api/authentication";

    public async Task<DateTime> LoginAsync(LoginViewModel loginViewModel)
    {
        var authenticationViewModel = await PostAsync<AuthenticationViewModel>("login", loginViewModel);
        await tokenService.SetTokensAsync(authenticationViewModel);
        await authenticationStateProvider.GetAuthenticationStateAsync();

        return authenticationViewModel.AuthExpiration;
    }

    public async Task LogoutAsync()
    {
        try
        {
            await DeleteAsync("revoke");
        }
        catch (Exception)
        {
        }

        await tokenService.RemoveTokensAsync();
        await authenticationStateProvider.GetAuthenticationStateAsync();
    }

    public async Task RegisterAsync(RegisterViewModel registerViewModel)
    {
        await PostAsync("register", registerViewModel);
    }
}
