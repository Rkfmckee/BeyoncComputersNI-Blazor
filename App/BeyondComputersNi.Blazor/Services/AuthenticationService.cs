using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels;
using Blazored.LocalStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Services;

public class AuthenticationService(IHttpClientFactory httpClientFactory, IConfiguration configuration,
    ILocalStorageService localStorage) : IAuthenticationService
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
        var response = await httpClientFactory.CreateClient(configuration["Api:HttpClient"] ?? "").PostAsync("api/authentication/login", JsonContent.Create(login));
        if (!response.IsSuccessStatusCode) throw new UnauthorizedAccessException(response.StatusCode.ToString());

        var content = await response.Content.ReadFromJsonAsync<AuthenticationViewModel>();
        if (content is null) throw new InvalidDataException();

        await localStorage.SetItemAsync(AuthKey, content.AuthToken);

        LoginChanged?.Invoke(GetEmail(content.AuthToken));

        return content.AuthExpiration;
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
