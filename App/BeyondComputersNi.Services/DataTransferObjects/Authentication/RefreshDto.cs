namespace BeyondComputersNi.Services.DataTransferObjects.Authentication;

public class RefreshDto
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
