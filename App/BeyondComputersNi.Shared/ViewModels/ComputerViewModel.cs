namespace BeyondComputersNi.Shared.ViewModels;

public class ComputerViewModel
{
    public required string Identifier { get; set; }
    public string? Motherboard { get; set; }
    public string? CPU { get; set; }
    public string? CPUCooler { get; set; }
    public string? Memory { get; set; }
    public string? Storage { get; set; }
    public string? GPU { get; set; }
    public string? PSU { get; set; }
    public string? Case { get; set; }
}
