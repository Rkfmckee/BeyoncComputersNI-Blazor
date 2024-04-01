using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Net.Http;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Authentication;

public class BcniAuthenticationStateProvider(ITokenService tokenService, IRefreshService refreshService) : AuthenticationStateProvider
{
    private JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

    public async override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var authTokenString = await tokenService.GetAuthTokenAsync();

        if (TokenIsEmpty(authTokenString))
            return Unauthorized();

        var authToken = tokenHandler.ReadJwtToken(authTokenString);

        if (authToken is null)
            return Unauthorized();

        if (TokenIsExpired(authToken))
            return await RefreshExpiredToken(authTokenString!);

        return Authorized(authToken);
    }

    private async Task<AuthenticationState> RefreshExpiredToken(string authTokenString)
    {
        var refreshTokenString = await tokenService.GetRefreshTokenAsync();

        if (TokenIsEmpty(refreshTokenString))
            return Unauthorized();

        var refreshToken = tokenHandler.ReadJwtToken(refreshTokenString);

        if (refreshToken is null)
            return Unauthorized();

        if (TokenIsExpired(refreshToken))
            return Unauthorized();

        var refreshedTokens = await refreshService.RefreshAsync(authTokenString, refreshTokenString!);

        if (refreshedTokens is null)
            return Unauthorized();

        await tokenService.SetTokensAsync(refreshedTokens);

        return Authorized(tokenHandler.ReadJwtToken(await tokenService.GetAuthTokenAsync()));
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
