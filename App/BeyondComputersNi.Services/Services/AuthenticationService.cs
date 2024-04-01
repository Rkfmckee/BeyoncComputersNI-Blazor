using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects.Authentication;
using BeyondComputersNi.Services.Interfaces;
using BeyondComputersNi.Shared.Enums;
using BeyondComputersNi.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeyondComputersNi.Services.Services;

public class AuthenticationService(IRepository<User> userRepository, IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor) : IAuthenticationService
{
    public async Task<AuthenticationDto?> AuthenticateAsync(string email, User? user = null)
    {
        if (user is null)
        {
            user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);
            if (user is null) return null;
        }

        var authToken = GenerateToken(email, TokenType.Auth);
        var authTokenString = new JwtSecurityTokenHandler().WriteToken(authToken);

        var refreshToken = GenerateToken(email, TokenType.Refresh);
        var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

        user.RefreshToken = refreshTokenString;
        await userRepository.SaveChangesAsync();

        return new AuthenticationDto
        {
            AuthToken = authTokenString,
            AuthExpiration = authToken.ValidTo,
            RefreshToken = refreshTokenString,
            RefreshExpiration = refreshToken.ValidTo
        };
    }

    public async Task<AuthenticationDto?> RefreshAsync(RefreshDto refreshDto)
    {
        var principal = GetClaimsPrincipalFromExpiredToken(refreshDto.AuthToken);
        var email = principal?.GetEmail();

        if (email is null) return null;

        var authToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshDto.AuthToken);
        var refreshToken = new JwtSecurityTokenHandler().ReadJwtToken(refreshDto.RefreshToken);

        var user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);

        if (user is null || 
            user.RefreshToken != refreshDto.RefreshToken ||
            refreshToken.ValidTo < DateTime.UtcNow ||
            authToken.ValidTo > DateTime.UtcNow)
            return null;

        return await AuthenticateAsync(email, user);
    }

    public async Task<bool> RevokeAsync()
    {
        var email = httpContextAccessor.HttpContext.User.GetEmail();
        if (email is null) return false;

        var user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);
        if (user is null) return false;

        user.RefreshToken = null;

        await userRepository.SaveChangesAsync();
        return true;
    }

    private JwtSecurityToken GenerateToken(string email, TokenType tokenType)
    {
        var secretKey = (tokenType == TokenType.Auth ?
            configuration["Jwt:AuthSecret"] :
            configuration["Jwt:RefreshSecret"]) ??
            throw new InvalidOperationException("Secret not configured");

        var expiryTime = tokenType == TokenType.Auth ?
            configuration["Jwt:AuthExpiryMinutes"] :
            configuration["Jwt:RefreshExpiryMinutes"] ??
             throw new InvalidOperationException("Expiry time not configured");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));

        return new JwtSecurityToken(
            issuer: configuration["Jwt:ValidIssuer"],
            audience: configuration["Jwt:ValidAudience"],
            expires: DateTime.UtcNow.AddMinutes(int.Parse(expiryTime!)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: claims);
    }

    private ClaimsPrincipal? GetClaimsPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = configuration["Jwt:ValidIssuer"],
            ValidAudience = configuration["Jwt:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                               configuration["Jwt:AuthSecret"] ??
                               throw new InvalidOperationException("Secret not configured"))),
            ValidateLifetime = false
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out _);
    }
}
