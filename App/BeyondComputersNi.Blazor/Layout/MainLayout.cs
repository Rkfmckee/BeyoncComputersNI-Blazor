using MudBlazor;

namespace BeyondComputersNi.Blazor.Layout;

public partial class MainLayout
{
    private bool DrawerOpen { get; set; }
    private MudTheme BcniTheme => new BcniTheme().Default;
}
