using System.Security.Claims;

namespace BeyondComputersNi.Shared.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetEmail(this ClaimsPrincipal? claimsPrincipal)
    {
        return claimsPrincipal?.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value;
    }
}
