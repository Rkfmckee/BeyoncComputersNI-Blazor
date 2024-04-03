using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.ViewModels.Authentication;

public class RegisterViewModel
{
    [EmailAddress]
    [Required(ErrorMessage = "Email address is required.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "You must confirm your password.")]
    public string? ConfirmPassword { get; set; }

    public string? Name { get; set; }
}
