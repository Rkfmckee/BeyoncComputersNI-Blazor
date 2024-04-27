namespace BeyondComputersNi.Api.ViewModels.Authentication;

public class RefreshViewModel
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
