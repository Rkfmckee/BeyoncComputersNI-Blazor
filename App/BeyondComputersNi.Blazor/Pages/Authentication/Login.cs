using BeyondComputersNi.Blazor.Interfaces;
using BeyondComputersNi.Blazor.Pages.Status;
using BeyondComputersNi.Blazor.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.Authentication;

public partial class Login : IDisposable
{
    public const string PageUrl = "/Login";

    [SupplyParameterFromQuery]
    private bool LoggedOut { get; set; }

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    private LoginViewModel? LoginViewModel { get; set; }
    private EditContext? EditContext { get; set; }
    private bool HasErrors { get; set; }

    protected override void OnInitialized()
    {
        if (AuthenticationService is null) NavigationManager!.NavigateTo(Error.PageUrl);

        LoginViewModel = new LoginViewModel();
        EditContext = new EditContext(LoginViewModel);
        EditContext.OnValidationStateChanged += HandleValidationStateChanged;
    }

    private void HandleValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        if (EditContext is null) return;

        HasErrors = EditContext?.GetValidationMessages().Any() ?? false;
        StateHasChanged();
    }

    private async void OnValidSubmit(EditContext context)
    {
        if (LoginViewModel is null) return;
        HasErrors = false;

        try
        {
            await AuthenticationService.LoginAsync(LoginViewModel);
            NavigationManager!.NavigateTo(Home.PageUrl);
            Snackbar.Add("Login successful", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }

        StateHasChanged();
    }

    private void OnInvalidSubmit(EditContext context)
    {
        HasErrors = true;
        StateHasChanged();
    }

    public void Dispose()
    {
        if (EditContext is null) return;

        EditContext.OnValidationStateChanged -= HandleValidationStateChanged;
    }
}
