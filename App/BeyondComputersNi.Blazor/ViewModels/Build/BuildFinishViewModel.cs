namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildFinishViewModel
{
    public BuildFinishViewModel(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
    public string? Identifier { get; set; }
    public int? UserId { get; set; }
}
