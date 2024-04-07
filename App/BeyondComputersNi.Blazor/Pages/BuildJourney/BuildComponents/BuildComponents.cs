using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Shared.ViewModels.Build;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.BuildJourney.BuildComponents;

public partial class BuildComponents : Form
{
    public const string PageUrl = "/Build/{id:int}/Components";

    [Parameter]
    public int Id { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar? Snackbar { get; set; }

    private BuildComponentsViewModel? BuildComponentsViewModel { get; set; }

    protected override void OnInitialized()
    {
        //if (AuthenticationService is null) NavigationManager!.NavigateTo(Error.PageUrl);

        BuildComponentsViewModel = new BuildComponentsViewModel();
        InitializeForm(BuildComponentsViewModel);
    }

    protected override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);
    }
}
