using MudBlazor;

namespace BeyondComputersNi.Blazor.Layout;

public class BcniTheme
{
    public MudTheme Default = new MudTheme
    {
        Palette = new PaletteLight
        {
            Primary = Colors.LightBlue.Default,
            Secondary = Colors.Teal.Lighten2,
            AppbarBackground = Colors.Shades.White,
            AppbarText = Colors.Grey.Darken4,
            DrawerBackground = Colors.Shades.White
        },

        Typography = new Typography
        {

        },

        LayoutProperties = new LayoutProperties
        {

        },

        Shadows = new Shadow
        {

        }
    };
}
