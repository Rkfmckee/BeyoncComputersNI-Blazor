using Microsoft.AspNetCore.Components;

namespace BeyondComputersNi.Blazor.Extensions;

public static class NavigationManagerExtensions
{
    public static string UrlWithoutBase(this NavigationManager navigationManager)
    {
        return navigationManager.Uri.Remove(0, navigationManager.BaseUri.TrimEnd('/').Length);
    }
}
