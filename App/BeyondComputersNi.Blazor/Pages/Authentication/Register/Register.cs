using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Shared.ViewModels.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.Authentication.Register;

public partial class Register : IDisposable
{
    public const string PageUrl = "/Register";

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    private RegisterViewModel? RegisterViewModel { get; set; }
    private EditContext? EditContext { get; set; }
    private bool HasErrors { get; set; }

    protected override void OnInitialized()
    {
        RegisterViewModel = new RegisterViewModel();
        EditContext = new EditContext(RegisterViewModel);
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
        if (RegisterViewModel is null) return;
        HasErrors = false;

        try
        {
            //await AuthenticationService.LoginAsync();

            NavigationManager!.NavigateTo(Home.PageUrl);
            Snackbar.Add("Registration successful", Severity.Success);
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
