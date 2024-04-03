namespace BeyondComputersNi.Shared.ViewModels.Authentication;

public class RefreshViewModel
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
