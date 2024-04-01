using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.ViewModels;
using Blazored.LocalStorage;

namespace BeyondComputersNi.Blazor.Services.Authentication;

public class TokenService(ILocalStorageService localStorage) : ITokenService
{
    private const string AuthTokenKey = "tok";
    private const string RefreshTokenKey = "ref";

    public async ValueTask<string?> GetAuthTokenAsync() =>
        await localStorage.GetItemAsync<string>(AuthTokenKey);

    public async ValueTask<string?> GetRefreshTokenAsync() =>
        await localStorage.GetItemAsync<string>(RefreshTokenKey);

    public async Task SetTokensAsync(AuthenticationViewModel authenticationViewModel)
    {
        await localStorage.SetItemAsync(AuthTokenKey, authenticationViewModel.AuthToken);
        await localStorage.SetItemAsync(RefreshTokenKey, authenticationViewModel.RefreshToken);
    }

    public async Task SetTokensAsync(RefreshViewModel refreshViewModel)
    {
        await localStorage.SetItemAsync(AuthTokenKey, refreshViewModel.AuthToken);
        await localStorage.SetItemAsync(RefreshTokenKey, refreshViewModel.RefreshToken);
    }

    public async Task RemoveTokensAsync()
    {
        await localStorage.RemoveItemAsync(AuthTokenKey);
        await localStorage.RemoveItemAsync(RefreshTokenKey);
    }
}
