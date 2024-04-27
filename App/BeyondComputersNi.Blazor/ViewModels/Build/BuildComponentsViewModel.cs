using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildComponentsViewModel
{
    public string? SelectMotherboard { get; set; }
    public string? EnterMotherboard { get; set; }

    [Required(ErrorMessage = "You must select a Motherboard")]
    public string? Motherboard => !string.IsNullOrWhiteSpace(EnterMotherboard) ? EnterMotherboard : SelectMotherboard;

    public string? SelectCPU { get; set; }
    public string? EnterCPU { get; set; }

    [Required(ErrorMessage = "You must select a CPU")]
    public string? CPU => !string.IsNullOrWhiteSpace(EnterCPU) ? EnterCPU : SelectCPU;

    public string? SelectCPUCooler { get; set; }
    public string? EnterCPUCooler { get; set; }

    [Required(ErrorMessage = "You must select a CPU cooler")]
    public string? CPUCooler => !string.IsNullOrWhiteSpace(EnterCPUCooler) ? EnterCPUCooler : SelectCPUCooler;

    public string? SelectMemory { get; set; }
    public string? EnterMemory { get; set; }

    [Required(ErrorMessage = "You must select some Memory")]
    public string? Memory => !string.IsNullOrWhiteSpace(EnterMemory) ? EnterMemory : SelectMemory;

    public string? SelectStorage { get; set; }
    public string? EnterStorage { get; set; }

    [Required(ErrorMessage = "You must select some Storage")]
    public string? Storage => !string.IsNullOrWhiteSpace(EnterStorage) ? EnterStorage : SelectStorage;

    public string? SelectGPU { get; set; }
    public string? EnterGPU { get; set; }

    [Required(ErrorMessage = "You must select a GPU")]
    public string? GPU => !string.IsNullOrWhiteSpace(EnterGPU) ? EnterGPU : SelectGPU;

    public string? SelectPSU { get; set; }
    public string? EnterPSU { get; set; }

    [Required(ErrorMessage = "You must select a PSU")]
    public string? PSU => !string.IsNullOrWhiteSpace(EnterPSU) ? EnterPSU : SelectPSU;
}