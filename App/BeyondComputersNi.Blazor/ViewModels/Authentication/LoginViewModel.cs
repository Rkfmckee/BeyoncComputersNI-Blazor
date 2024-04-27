using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.ViewModels.Authentication;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email address is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }
}
