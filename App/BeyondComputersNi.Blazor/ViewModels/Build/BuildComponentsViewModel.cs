using BeyondComputersNi.Blazor.Pages.BuildJourney.BuildPeripherals;
using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildComponentsViewModel
{
    public BuildComponentsViewModel(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string PeripheralsUrl => BuildPeripherals.PageUrl.Replace("{id:int}", Id.ToString());

    [Required(ErrorMessage = "You must select a Motherboard")]
    public string? Motherboard { get; set; }

    [Required(ErrorMessage = "You must select a CPU")]
    public string? CPU { get; set; }

    [Required(ErrorMessage = "You must select a CPU cooler")]
    public string? CPUCooler { get; set; }

    [Required(ErrorMessage = "You must select some Memory")]
    public string? Memory { get; set; }

    [Required(ErrorMessage = "You must select some Storage")]
    public string? Storage { get; set; }

    [Required(ErrorMessage = "You must select a GPU")]
    public string? GPU { get; set; }

    [Required(ErrorMessage = "You must select a PSU")]
    public string? PSU { get; set; }
}

// Add FluentValidation