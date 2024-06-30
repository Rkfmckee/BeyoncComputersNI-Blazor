namespace BeyondComputersNi.Services.DataTransferObjects.Build;

public class BuildDto
{
    public required string BuildNumber { get; set; }
    public string? Identifier { get; set; }
    public int? UserId { get; set; }

    public string? Motherboard { get; set; }
    public string? CPU { get; set; }
    public string? CPUCooler { get; set; }
    public string? Memory { get; set; }
    public string? Storage { get; set; }
    public string? GPU { get; set; }
    public string? PSU { get; set; }

    public string? Case { get; set; }
    public string? Keyboard { get; set; }
    public string? Mouse { get; set; }
    public string? Monitor { get; set; }
    public string? Speakers { get; set; }
}
