using BeyondComputersNi.Blazor.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BeyondComputersNi.Blazor.Pages.Authentication;

public partial class Login
{
    public const string PageUrl = "/Login";

    private LoginViewModel? LoginViewModel { get; set; }
    private bool HasErrors { get; set; }

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
