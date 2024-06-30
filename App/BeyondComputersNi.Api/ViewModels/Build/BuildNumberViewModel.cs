namespace BeyondComputersNi.Api.ViewModels.Build;

public class BuildNumberViewModel
{
    public BuildNumberViewModel(string? value)
    {
        Value = value;
    }

    public string? Value { get; set; }
}
