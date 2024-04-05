using BeyondComputersNi.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.ViewModels.Authentication;

public class RegisterViewModel
{
    [Required(ErrorMessage = "Email address is required.")]
    [Email]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [ComplexPassword]
    public string? Password { get; set; }

    [Required(ErrorMessage = "You must confirm your password.")]
    [CompareProperty(nameof(Password), ErrorMessage = "Your passwords don't match. Please enter the same password you entered above.")]
    public string? ConfirmPassword { get; set; }

    public string? Name { get; set; }
}
