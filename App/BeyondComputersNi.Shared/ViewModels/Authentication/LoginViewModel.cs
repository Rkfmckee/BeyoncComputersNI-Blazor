using BeyondComputersNi.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.ViewModels.Authentication;

public class LoginViewModel
{
    [Email]
    [Required(ErrorMessage = "Email address is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
