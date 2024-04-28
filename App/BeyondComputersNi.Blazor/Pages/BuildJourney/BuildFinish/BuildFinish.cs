using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.ViewModels.Build;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Security.Claims;

namespace BeyondComputersNi.Blazor.Pages.BuildJourney.BuildFinish;

public partial class BuildFinish : Form
{
    public const string PageUrl = "/Build/{id:int}/Finish";

    [Parameter]
    public int Id { get; set; }

    [CascadingParameter]
    private Task<AuthenticationState>? AuthenticationState { get; set; }

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

        if (!await BuildService!.BuildExists(Id))
            NavigationManager!.NavigateTo("");

        var buildNumber = await BuildService.GetBuildNumber(Id);
        if (!string.IsNullOrEmpty(buildNumber?.Value)) BuildNumber = buildNumber.Value;

        BuildFinishViewModel = new BuildFinishViewModel(Id);
        InitializeForm(BuildFinishViewModel);

        LoadingForm = false;
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);
        Submitting = true;

        await GetCurrentUserIdAsync();

        try
        {
            await BuildService!.FinishBuild(BuildFinishViewModel!);
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

    private async Task GetCurrentUserIdAsync()
    {
        if (AuthenticationState is null || BuildFinishViewModel is null) return;

        var authState = await AuthenticationState;
        var user = authState?.User;

        if (user?.Identity is null || !user.Identity.IsAuthenticated) return;

        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return;

        BuildFinishViewModel!.UserId = int.Parse(userId);
    }
}
