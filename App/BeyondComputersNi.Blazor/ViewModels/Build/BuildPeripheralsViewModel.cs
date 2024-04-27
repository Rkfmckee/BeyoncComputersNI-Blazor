using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildPeripheralsViewModel
{
    public BuildPeripheralsViewModel(int id)
    {
        Id = id;
    }

    public int Id { get; set; }

    public string? SelectCase { get; set; }
    public string? EnterCase { get; set; }

    [Required(ErrorMessage = "You must select a Case")]
    public string? Case => !string.IsNullOrWhiteSpace(EnterCase) ? EnterCase : SelectCase;

    public string? SelectKeyboard { get; set; }
    public string? EnterKeyboard { get; set; }

    [Required(ErrorMessage = "You must select a Keyboard")]
    public string? Keyboard => !string.IsNullOrWhiteSpace(EnterKeyboard) ? EnterKeyboard : SelectKeyboard;

    public string? SelectMouse { get; set; }
    public string? EnterMouse { get; set; }

    [Required(ErrorMessage = "You must select a Mouse")]
    public string? Mouse => !string.IsNullOrWhiteSpace(EnterMouse) ? EnterMouse : SelectMouse;

    public string? SelectMonitor { get; set; }
    public string? EnterMonitor { get; set; }

    [Required(ErrorMessage = "You must select some Monitor")]
    public string? Monitor => !string.IsNullOrWhiteSpace(EnterMonitor) ? EnterMonitor : SelectMonitor;

    public string? SelectSpeakers { get; set; }
    public string? EnterSpeakers { get; set; }

    [Required(ErrorMessage = "You must select some Speakers")]
    public string? Speakers => !string.IsNullOrWhiteSpace(EnterSpeakers) ? EnterSpeakers : SelectSpeakers;
}