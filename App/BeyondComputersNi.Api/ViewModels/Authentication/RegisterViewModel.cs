using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Api.ViewModels.Authentication;

public class RegisterViewModel
{
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Name { get; set; }
}
