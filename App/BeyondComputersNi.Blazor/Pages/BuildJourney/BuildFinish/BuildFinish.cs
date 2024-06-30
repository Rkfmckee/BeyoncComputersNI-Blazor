using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Extensions;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.BuildJourney.BuildFinish;

public partial class BuildFinish : Form
{
    public const string PageUrl = "/Build/{id:int}/Finish";

    [Parameter]
    public int Id { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar? Snackbar { get; set; }

    [Inject]
    private IBuildService? BuildService { get; set; }

    private BuildFinishViewModel? BuildFinishViewModel { get; set; }
    private string? BuildNumber { get; set; } = "BLD/0000/00/00/0";
    private bool LoadingForm { get; set; }
    private bool Submitting { get; set; }

    protected async override Task OnInitializedAsync()
    {
        LoadingForm = true;

        if (!await BuildService!.BuildExistsAsync(Id))
            NavigationManager!.NavigateTo("");

        var buildNumber = await BuildService.GetBuildNumberAsync(Id);
        if (!string.IsNullOrEmpty(buildNumber?.Value)) BuildNumber = buildNumber.Value;

        BuildFinishViewModel = new BuildFinishViewModel(Id);
        InitializeForm(BuildFinishViewModel);

        LoadingForm = false;
    }

    private void OnLoginButtonClicked()
    {
        NavigationManager?.NavigateTo(NavigationManager.LoginUrlWithRedirect());
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);
        Submitting = true;

        try
        {
            await BuildService!.FinishBuildAsync(BuildFinishViewModel!);
            NavigationManager!.NavigateTo("");
            Snackbar?.Add("Build complete", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar?.Add(ex.Message, Severity.Error);
        }

        Submitting = false;
        StateHasChanged();
    }
}
