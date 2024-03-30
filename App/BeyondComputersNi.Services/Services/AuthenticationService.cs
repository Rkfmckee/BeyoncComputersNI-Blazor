using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using BeyondComputersNi.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace BeyondComputersNi.Services.Services;

public class AuthenticationService(IRepository<User> userRepository, IConfiguration configuration,
    IHttpContextAccessor httpContextAccessor) : IAuthenticationService
{
    public async Task<AuthenticationDto?> AuthenticateAsync(string email, User? user = null)
    {
        if (user is null)
        {
            user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);
            if (user is null) throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        var authToken = GenerateAuthToken(email);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiry = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:RefreshExpiryMinutes"]
                     ?? throw new InvalidOperationException("Expiry time not configured")));

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = refreshTokenExpiry;
        await userRepository.SaveChangesAsync();

        return new AuthenticationDto
        {
            AuthToken = new JwtSecurityTokenHandler().WriteToken(authToken),
            AuthExpiration = authToken.ValidTo,
            RefreshToken = refreshToken,
            RefreshExpiration = refreshTokenExpiry
        };
    }

    public async Task<AuthenticationDto?> RefreshAsync(RefreshDto refreshDto)
    {
        var principal = GetClaimsPrincipalFromExpiredToken(refreshDto.AuthToken);
        var email = principal?.GetEmail();

        if (email is null) throw new HttpResponseException(HttpStatusCode.Unauthorized);

        var user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);
        if (user is null || user.RefreshToken != refreshDto.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            throw new HttpResponseException(HttpStatusCode.Unauthorized);

        return await AuthenticateAsync(email, user);
    }

    public async Task<bool> RevokeAsync()
    {
        var email = httpContextAccessor.HttpContext.User.GetEmail();
        if (email is null) throw new HttpResponseException(HttpStatusCode.Unauthorized);

        var user = await userRepository.Get().SingleOrDefaultAsync(u => u.Email == email);
        if (user is null) throw new HttpResponseException(HttpStatusCode.NotFound);

        user.RefreshToken = null;
        user.RefreshTokenExpiry = null;

        await userRepository.SaveChangesAsync();
        return true;
    }

    private JwtSecurityToken GenerateAuthToken(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration["Jwt:AuthSecret"] ?? throw new InvalidOperationException("Secret not configured")));

        return new JwtSecurityToken(
            issuer: configuration["Jwt:ValidIssuer"],
            audience: configuration["Jwt:ValidAudience"],
            expires: DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AuthExpiryMinutes"]
                     ?? throw new InvalidOperationException("Expiry time not configured"))),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: claims);
    }

    private string GenerateRefreshToken()
    {
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        var randomNumber = new byte[64];

        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
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
