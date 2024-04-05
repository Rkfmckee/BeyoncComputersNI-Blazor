using BeyondComputersNi.Shared.ViewModels;

namespace BeyondComputersNi.Blazor.Interfaces;
public interface IComputerService
{
    Task<List<ComputerViewModel>> GetAllComputers();
}