using AutoMapper;
using BeyondComputersNi.Api.ViewModels;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class ComputerViewModelProfile : Profile
{
    public ComputerViewModelProfile()
    {
        CreateMap<ComputerDto, ComputerViewModel>();
    }
}
