using AutoMapper;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Shared.ViewModels;

namespace BeyondComputersNi.Services.MappingProfiles;

public class ComputerProfiles : Profile
{
    public ComputerProfiles()
    {
        CreateMap<ComputerDto, ComputerViewModel>();
    }
}
