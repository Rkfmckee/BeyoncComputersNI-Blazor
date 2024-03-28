using BeyondComputersNi.Blazor.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.Authentication;

public partial class Login
{
    public const string PageUrl = "/Login";

    private LoginViewModel LoginViewModel { get; set; }
    private bool HasErrors { get; set; }

    private bool PasswordIsVisible { get; set; }
    private InputType PasswordInputType { get; set; } = InputType.Password;
    private string PasswordVisibilityIcon { get; set; } = Icons.Material.Filled.Visibility;

    private void TogglePasswordVisibility()
    {
        PasswordIsVisible = !PasswordIsVisible;
        PasswordInputType = PasswordIsVisible ? InputType.Text : InputType.Password;
        PasswordVisibilityIcon = PasswordIsVisible ? Icons.Material.Filled.VisibilityOff : Icons.Material.Filled.Visibility;
    }

    protected override void OnInitialized()
    {
        LoginViewModel = new LoginViewModel();
    }

    private void OnValidSubmit(EditContext context)
    {
        HasErrors = false;

        StateHasChanged();
    }

    private void OnInvalidSubmit(EditContext context)
    {
        HasErrors = true;
    }
}
