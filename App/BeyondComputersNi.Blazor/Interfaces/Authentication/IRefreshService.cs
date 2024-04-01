using BeyondComputersNi.Blazor.ViewModels.Authentication;

namespace BeyondComputersNi.Blazor.Interfaces.Authentication;
public interface IRefreshService
{
    Task<AuthenticationViewModel?> RefreshAsync(string authTokenString, string refreshTokenString);
}