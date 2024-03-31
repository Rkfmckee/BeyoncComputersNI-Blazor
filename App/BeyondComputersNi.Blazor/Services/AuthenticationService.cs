using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Services;

public class AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
    ILocalStorageService localStorage) : BaseService(httpClientFactory, configuration), IAuthenticationService
{
    private const string AuthKey = nameof(AuthKey);
    private string? authTokenCache;

    public async ValueTask<string?> GetAuthTokenAsync()
    {
        if (string.IsNullOrEmpty(authTokenCache))
            authTokenCache = await localStorage.GetItemAsync<string>(AuthKey);

        return authTokenCache;
    }

    public event Action<string?>? LoginChanged;

    public async Task<DateTime> LoginAsync(LoginViewModel login)
    {
        var authenticationViewModel = await PostAsync<AuthenticationViewModel>("api/authentication/login", login);

        await localStorage.SetItemAsync(AuthKey, authenticationViewModel.AuthToken);

        LoginChanged?.Invoke(GetEmail(authenticationViewModel.AuthToken));

        return authenticationViewModel.AuthExpiration;
    }

    public async Task LogoutAsync()
    {
        await localStorage.RemoveItemAsync(AuthKey);
        authTokenCache = null;

        LoginChanged?.Invoke(null);
    }

    private static string? GetEmail(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}
