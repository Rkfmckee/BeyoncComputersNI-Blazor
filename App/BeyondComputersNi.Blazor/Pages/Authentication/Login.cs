using BeyondComputersNi.Blazor.ViewModels;
using Microsoft.AspNetCore.Components.Forms;

namespace BeyondComputersNi.Blazor.Pages.Authentication;

public partial class Login : IDisposable
{
    public const string PageUrl = "/Login";

    private LoginViewModel? LoginViewModel { get; set; }
    private EditContext? EditContext { get; set; }
    private bool HasErrors { get; set; }

    protected override void OnInitialized()
    {
        LoginViewModel = new LoginViewModel();
        EditContext = new EditContext(LoginViewModel);
        EditContext.OnFieldChanged += HandleFieldChanged;
    }

    private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        if (EditContext is null) return;

        HasErrors = !EditContext.Validate();
    }

    private void OnValidSubmit(EditContext context)
    {
        HasErrors = false;
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

        EditContext.OnFieldChanged -= HandleFieldChanged;
    }
}
