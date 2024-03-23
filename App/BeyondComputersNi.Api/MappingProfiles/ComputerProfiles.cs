using AutoMapper;
using BeyondComputersNi.Api.ViewModels;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class ComputerProfiles : Profile
{
    public ComputerProfiles()
    {
        CreateMap<ComputerDto, ComputerViewModel>();
    }
}
