using BeyondComputersNi.Blazor.ViewModels.Authentication;

namespace BeyondComputersNi.Blazor.Interfaces.Authentication;
public interface IAuthenticationService
{
    Task<DateTime> LoginAsync(LoginViewModel loginViewModel);
    Task LogoutAsync();
    Task RegisterAsync(RegisterViewModel registerViewModel);
}