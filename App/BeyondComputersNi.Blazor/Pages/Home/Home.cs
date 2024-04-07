using BeyondComputersNi.Blazor.Pages.BuildJourney;
using Microsoft.AspNetCore.Components;

namespace BeyondComputersNi.Blazor.Pages.Home;

public partial class Home
{
    public const string PageUrl = "/";
    public const string PageUrlAlt = "/Home";

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    private void OnBuildButtonClicked()
    {
        NavigationManager.NavigateTo(Build.PageUrl);
    }
}
