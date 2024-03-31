using BeyondComputersNi.Blazor.ViewModels;

namespace BeyondComputersNi.Blazor.Interfaces;
public interface IAuthenticationService
{
    event Action<string?>? LoginChanged;
    ValueTask<string?> GetAuthTokenAsync();
    ValueTask<string?> GetRefreshTokenAsync();
    Task<DateTime> LoginAsync(LoginViewModel login);
    Task LogoutAsync();
    Task RefreshAsync();
}