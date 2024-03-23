using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Api.ViewModels.Authentication;

public class LoginViewModel
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}
