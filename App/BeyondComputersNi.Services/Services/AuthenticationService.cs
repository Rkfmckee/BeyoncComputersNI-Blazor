using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BeyondComputersNi.Services.Services;

public class AuthenticationService(IConfiguration configuration) : IAuthenticationService
{
    public AuthenticationDto? Authenticate(string email)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration["Jwt:AuthSecret"] ?? throw new InvalidOperationException("Secret not configured")));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:ValidIssuer"],
            audience: configuration["Jwt:ValidAudience"],
            expires: DateTime.UtcNow.AddHours(int.Parse(configuration["Jwt:AuthExpiryHours"] 
                     ?? throw new InvalidOperationException("Expiry time not configured"))),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            claims: claims);

        return new AuthenticationDto
        {
            AuthToken = new JwtSecurityTokenHandler().WriteToken(token),
            AuthExpiration = token.ValidTo,
            RefreshToken = "",
            RefreshExpiration = new DateTime()
        };
    }
}
