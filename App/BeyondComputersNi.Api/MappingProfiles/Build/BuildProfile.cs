using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Build;
using BeyondComputersNi.Services.DataTransferObjects.Build;

namespace BeyondComputersNi.Api.MappingProfiles.Build;

public class BuildProfile : Profile
{
    public BuildProfile()
    {
        CreateMap<BuildDto, BuildViewModel>();

        CreateMap<BuildComponentsViewModel, BuildComponentsDto>();
        CreateMap<BuildPeripheralsViewModel, BuildPeripheralsDto>();
        CreateMap<BuildFinishViewModel, BuildFinishDto>();
    }
}
