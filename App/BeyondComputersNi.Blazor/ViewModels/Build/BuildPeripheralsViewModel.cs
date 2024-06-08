using BeyondComputersNi.Blazor.Pages.BuildJourney.BuildFinish;
using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildPeripheralsViewModel
{
    public BuildPeripheralsViewModel(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string FinishUrl => BuildFinish.PageUrl.Replace("{id:int}", Id.ToString());

    [Required(ErrorMessage = "You must select a Case")]
    public string? Case { get; set; }

    [Required(ErrorMessage = "You must select a Keyboard")]
    public string? Keyboard { get; set; }

    [Required(ErrorMessage = "You must select a Mouse")]
    public string? Mouse { get; set; }

    [Required(ErrorMessage = "You must select some Monitor")]
    public string? Monitor { get; set; }

    [Required(ErrorMessage = "You must select some Speakers")]
    public string? Speakers { get; set; }
}