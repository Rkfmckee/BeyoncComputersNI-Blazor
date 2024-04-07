using BeyondComputersNi.Blazor.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BeyondComputersNi.Blazor.Pages.BuildJourney.Components.BuildTimeline;

public partial class BuildTimeline
{
    [Parameter, EditorRequired]
    public BuildTimelineItem CurrentItem { get; set; }

    private static Color defaultColor = Color.Default;
    private static Color selectedColor = Color.Primary;
    private static Size defaultSize = Size.Small;
    private static Size selectedSize = Size.Medium;

    private Color ComponentsColor { get; set; } = defaultColor;
    private Size ComponentsSize { get; set; } = defaultSize;

    private Color PeripheralsColor { get; set; } = defaultColor;
    private Size PeripheralsSize { get; set; } = defaultSize;

    private Color FinishColor { get; set; } = defaultColor;
    private Size FinishSize { get; set; } = defaultSize;

    protected override void OnInitialized()
    {
        switch (CurrentItem)
        {
            case BuildTimelineItem.Components:
                ComponentsColor = selectedColor;
                ComponentsSize = selectedSize;
                break;

            case BuildTimelineItem.Peripherals:
                PeripheralsColor = selectedColor;
                PeripheralsSize = selectedSize;
                break;

            case BuildTimelineItem.Finish:
                FinishColor = selectedColor;
                FinishSize = selectedSize;
                break;
        }
    }
}
