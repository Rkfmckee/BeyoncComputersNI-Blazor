using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;
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

    [Inject]
    private IBuildService? BuildService { get; set; }

    private BuildComponentsViewModel? BuildComponentsViewModel { get; set; }
    private bool Submitting { get; set; }

    protected override void OnInitialized()
    {
        BuildComponentsViewModel = new BuildComponentsViewModel(Id);
        InitializeForm(BuildComponentsViewModel);
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);
        Submitting = true;

        try
        {
            await BuildService!.AddComponents(BuildComponentsViewModel!);
            //NavigationManager!.NavigateTo("");
            Snackbar?.Add("Success", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar?.Add(ex.Message, Severity.Error);
        }

        Submitting = false;
        StateHasChanged();
    }
}
