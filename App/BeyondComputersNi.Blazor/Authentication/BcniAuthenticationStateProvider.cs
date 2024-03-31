using BeyondComputersNi.Blazor.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Authentication;

public class BcniAuthenticationStateProvider(IAuthenticationService authenticationService) : AuthenticationStateProvider
{
    private JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authTokenString = await authenticationService.GetAuthTokenAsync();

        if (TokenIsEmpty(authTokenString))
            return Unauthorized();

        var authToken = tokenHandler.ReadJwtToken(authTokenString);

        if (authToken is null)
            return Unauthorized();

        if (TokenIsExpired(authToken))
            return await RefreshExpiredToken();

        return Authorized(authToken);
    }

    private async Task<AuthenticationState> RefreshExpiredToken()
    {
        var refreshTokenString = await authenticationService.GetRefreshTokenAsync();

        if (TokenIsEmpty(refreshTokenString))
            return Unauthorized();

        var refreshToken = tokenHandler.ReadJwtToken(refreshTokenString);

        if (refreshToken is null)
            return Unauthorized();

        if (TokenIsExpired(refreshToken))
            return Unauthorized();

        var refreshSucceeded = await authenticationService.RefreshAsync();

        if (!refreshSucceeded)
            return Unauthorized();

        return Authorized(tokenHandler.ReadJwtToken(await authenticationService.GetAuthTokenAsync()));
    }

    private bool TokenIsEmpty(string? token)
    {
        if (string.IsNullOrEmpty(token))
            return true;

        return false;
    }

    private bool TokenIsExpired(JwtSecurityToken token)
    {
        var x = token.ValidTo;
        var y = DateTime.UtcNow;

        if (token.ValidTo < DateTime.UtcNow)
            return true;

        return false;
    }

    private AuthenticationState Unauthorized()
    {
        return GetStateFromClaimsIdentity(new ClaimsIdentity());
    }

    private AuthenticationState Authorized(JwtSecurityToken token)
    {
        return GetStateFromClaimsIdentity(new ClaimsIdentity(token.Claims, "jwt"));

    }

    private AuthenticationState GetStateFromClaimsIdentity(ClaimsIdentity claimsIdentity)
    {
        var user = new ClaimsPrincipal(claimsIdentity);
        var state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }
}
