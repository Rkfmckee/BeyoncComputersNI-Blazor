using BeyondComputersNi.Blazor.ViewModels;

namespace BeyondComputersNi.Blazor.Interfaces.Authentication;
public interface IAuthenticationService
{
    Task<DateTime> LoginAsync(LoginViewModel login);
    Task LogoutAsync();
}