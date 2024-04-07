using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Shared.ViewModels.Build;

public class BuildComponentsViewModel
{
    public string? SelectMotherboard { get; set; }
    public string? EnterMotherboard { get; set; }

    [Required(ErrorMessage = "You must select a Motherboard")]
    public string? Motherboard => !string.IsNullOrEmpty(EnterMotherboard) ? EnterMotherboard : SelectMotherboard;

    public string? CPU { get; set; }
    public string? CPUCooler { get; set; }
    public string? Memory { get; set; }
    public string? Storage { get; set; }
    public string? GPU { get; set; }
    public string? PSU { get; set; }
}