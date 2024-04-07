namespace BeyondComputersNi.Dal.Entities;

public class Peripherals : Entity
{
    public string? Case { get; set; }
    public string? Keyboard { get; set; }
    public string? Mouse { get; set; }
    public string? Monitor { get; set; }
    public string? Speakers { get; set; }

    public required int ComputerId { get; set; }
    public required Computer Computer { get; set; }
}
