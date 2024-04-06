using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.Pages.Status;
using BeyondComputersNi.Shared.ViewModels.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.Authentication.Login;

public partial class Login : Form
{
    public const string PageUrl = "/Login";

    [SupplyParameterFromQuery]
    private bool NeedAuth { get; set; }

    [SupplyParameterFromQuery]
    private string? RedirectTo { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    private LoginViewModel? LoginViewModel { get; set; }
    private bool LoggingIn { get; set; }

    protected override void OnInitialized()
    {
        if (AuthenticationService is null) NavigationManager!.NavigateTo(Error.PageUrl);
        if (NeedAuth || !string.IsNullOrEmpty(RedirectTo)) Snackbar.Add("You need to log in to continue", Severity.Info);

        LoginViewModel = new LoginViewModel();
        InitializeForm(LoginViewModel);
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);

        var redirectUrl = string.IsNullOrEmpty(RedirectTo) ? Home.Home.PageUrl : RedirectTo;
        LoggingIn = true;

        try
        {
            await AuthenticationService.LoginAsync(LoginViewModel!);

            NavigationManager!.NavigateTo(redirectUrl);
            Snackbar.Add("Login successful", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }

        LoggingIn = false;
        StateHasChanged();
    }
}
