namespace BeyondComputersNi.Blazor.Interfaces;

public interface IBuildService
{
    string BaseUrl { get; }

    Task<int> CreateBuild();
}