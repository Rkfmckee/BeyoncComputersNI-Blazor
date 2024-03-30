using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;
using System.Security.Claims;

namespace BeyondComputersNi.Services.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationDto?> AuthenticateAsync(string email, User? user = null);
    Task<AuthenticationDto?> RefreshAsync(RefreshDto refreshDto);
    Task<bool> RevokeAsync();
}
