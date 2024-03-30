namespace BeyondComputersNi.Services.DataTransferObjects;

public class RefreshDto
{
    public required string AuthToken { get; set; }
    public required string RefreshToken { get; set; }
}
