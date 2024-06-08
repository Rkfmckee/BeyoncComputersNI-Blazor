using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.BuildJourney.BuildPeripherals;

public partial class BuildPeripherals : Form
{
    public const string PageUrl = "/Build/{id:int}/Peripherals";

    [Parameter]
    public int Id { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar? Snackbar { get; set; }

    [Inject]
    private IBuildService? BuildService { get; set; }

    private BuildPeripheralsViewModel? BuildPeripheralsViewModel { get; set; }
    private bool LoadingForm { get; set; }
    private bool Submitting { get; set; }

    protected async override Task OnInitializedAsync()
    {
        LoadingForm = true;

        if (!await BuildService!.BuildExistsAsync(Id))
            NavigationManager!.NavigateTo("");

        BuildPeripheralsViewModel = new BuildPeripheralsViewModel(Id);
        InitializeForm(BuildPeripheralsViewModel);

        LoadingForm = false;
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);
        Submitting = true;

        try
        {
            await BuildService!.AddPeripheralsAsync(BuildPeripheralsViewModel!);
            NavigationManager!.NavigateTo(BuildPeripheralsViewModel!.FinishUrl);
            Snackbar?.Add("Peripherals added successfully", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar?.Add(ex.Message, Severity.Error);
        }

        Submitting = false;
        StateHasChanged();
    }
}
