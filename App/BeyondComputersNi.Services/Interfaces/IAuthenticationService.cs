using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.Interfaces;

public interface IAuthenticationService
{
    AuthenticationDto? Authenticate(string email);
}
