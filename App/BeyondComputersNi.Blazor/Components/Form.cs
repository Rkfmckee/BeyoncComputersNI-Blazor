using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BeyondComputersNi.Blazor.Components;

public class Form : ComponentBase, IDisposable
{
    protected EditContext? EditContext { get; set; }
    protected bool HasErrors { get; set; }

    protected void InitializeForm(object model)
    {
        EditContext = new EditContext(model);
        EditContext.OnValidationStateChanged += HandleValidationStateChanged;
    }

    protected void HandleValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        if (EditContext is null) return;

        HasErrors = EditContext.GetValidationMessages().Any();
        StateHasChanged();
    }

    protected virtual void OnValidSubmit(EditContext context)
    {
        HasErrors = false;
        StateHasChanged();
    }

    protected virtual void OnInvalidSubmit(EditContext context)
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
