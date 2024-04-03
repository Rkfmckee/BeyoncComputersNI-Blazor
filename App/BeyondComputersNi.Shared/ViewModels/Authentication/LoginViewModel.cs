using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.ViewModels.Authentication;

public class LoginViewModel
{
    [EmailAddress]
    public string? Email { get; set; }
    public string? Password { get; set; }
}
