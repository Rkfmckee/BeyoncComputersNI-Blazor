using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Services;

public class AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
    ILocalStorageService localStorage) : BaseService(httpClientFactory, configuration), IAuthenticationService
{
    private const string AuthTokenKey = "tok";
    private string? authTokenCache;

    private const string RefreshTokenKey = "ref";
    private string? refreshTokenCache;
    
    public event Action<string?>? LoginChanged;

    public async ValueTask<string?> GetAuthTokenAsync()
    {
        if (string.IsNullOrEmpty(authTokenCache))
            authTokenCache = await localStorage.GetItemAsync<string>(AuthTokenKey);

        return authTokenCache;
    }

    public async ValueTask<string?> GetRefreshTokenAsync()
    {
        if (string.IsNullOrEmpty(refreshTokenCache))
            refreshTokenCache = await localStorage.GetItemAsync<string>(RefreshTokenKey);

        return refreshTokenCache;
    }


    public async Task<DateTime> LoginAsync(LoginViewModel login)
    {
        var authenticationViewModel = await PostAsync<AuthenticationViewModel>("api/authentication/login", login);

        await SetTokens(authenticationViewModel);

        LoginChanged?.Invoke(GetEmail(authenticationViewModel.AuthToken));

        return authenticationViewModel.AuthExpiration;
    }

    public async Task LogoutAsync()
    {
        await DeleteAsync("api/authentication/revoke");

        await localStorage.RemoveItemAsync(AuthTokenKey);
        authTokenCache = null;

        await localStorage.RemoveItemAsync(RefreshTokenKey);
        refreshTokenCache = null;

        LoginChanged?.Invoke(null);
    }

    public async Task RefreshAsync()
    {
        var authToken = await GetAuthTokenAsync();
        var refreshToken = await GetRefreshTokenAsync();

        if (authToken is null || refreshToken is null) throw new Exception("Unauthorized");

        var refreshViewModel = new RefreshViewModel
        {
            AuthToken = authToken,
            RefreshToken = refreshToken
        };

        var authenticationViewModel = await PostAsync<AuthenticationViewModel>("api/authentication/refresh", refreshViewModel);
        if (authenticationViewModel is null) await LogoutAsync();

        await SetTokens(authenticationViewModel!);
    }

    private async Task SetTokens(AuthenticationViewModel authenticationViewModel)
    {
        await localStorage.SetItemAsync(AuthTokenKey, authenticationViewModel.AuthToken);
        await localStorage.SetItemAsync(RefreshTokenKey, authenticationViewModel.RefreshToken);
    }

    private static string? GetEmail(string token)
    {
        var authToken = new JwtSecurityToken(token);
        return authToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    }
}
