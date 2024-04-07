namespace BeyondComputersNi.Dal.Entities;

public class Computer : Entity
{
    public required string Identifier { get; set; }
    public required string Motherboard { get; set; }
    public required string CPU { get; set; }
    public required string CPUCooler { get; set; }
    public required string Memory { get; set; }
    public required string Storage { get; set; }
    public required string GPU { get; set; }
    public required string PSU { get; set; }

    public required Peripherals Peripherals { get; set; }

    public int? UserId { get; set; }
    public User? User { get; set; }
}
