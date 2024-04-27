namespace BeyondComputersNi.Blazor.ViewModels.Build;

public class BuildViewModel
{
    public BuildViewModel(string componentsPage)
    {
        ComponentsPage = componentsPage;
    }

    public int Id { get; set; }
    public string? Number { get; set; }

    public string? ComponentsPage { get; set; }
    public string ComponentsUrl => ComponentsPage?.Replace("{id:int}", Id.ToString()) ?? "";
    public BuildComponentsViewModel? Components { get; set; }
}
