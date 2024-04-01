using BeyondComputersNi.Blazor.ViewModels.Authentication;

namespace BeyondComputersNi.Blazor.Interfaces.Authentication;
public interface ITokenService
{
    ValueTask<string?> GetAuthTokenAsync();
    ValueTask<string?> GetRefreshTokenAsync();
    Task SetTokensAsync(AuthenticationViewModel authenticationViewModel);
    Task RemoveTokensAsync();
}