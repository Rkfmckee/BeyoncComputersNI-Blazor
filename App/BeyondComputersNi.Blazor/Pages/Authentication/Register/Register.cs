using BeyondComputersNi.Blazor.Components;
using BeyondComputersNi.Blazor.Interfaces.Authentication;
using BeyondComputersNi.Blazor.Pages.Status;
using BeyondComputersNi.Shared.ViewModels.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.Authentication.Register;

public partial class Register : Form
{
    public const string PageUrl = "/Register";

    [Inject]
    private IAuthenticationService AuthenticationService { get; set; }

    [Inject]
    private NavigationManager? NavigationManager { get; set; }

    [Inject]
    private ISnackbar Snackbar { get; set; }

    private RegisterViewModel? RegisterViewModel { get; set; }

    protected override void OnInitialized()
    {
        if (AuthenticationService is null) NavigationManager!.NavigateTo(Error.PageUrl);

        RegisterViewModel = new RegisterViewModel();
        InitializeForm(RegisterViewModel);
    }

    protected async override void OnValidSubmit(EditContext context)
    {
        base.OnValidSubmit(context);

        try
        {
            await AuthenticationService.RegisterAsync(RegisterViewModel!);

            NavigationManager!.NavigateTo(Login.Login.PageUrl);
            Snackbar.Add("Registration successful, you can now log in", Severity.Success);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }
}
