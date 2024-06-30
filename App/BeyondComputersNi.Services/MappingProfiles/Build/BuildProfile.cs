using AutoMapper;
using BeyondComputersNi.Services.DataTransferObjects.Build;

namespace BeyondComputersNi.Services.MappingProfiles.Build;

public class BuildProfile : Profile
{
    public BuildProfile()
    {
        CreateMap<Dal.Entities.Build, BuildDto>();

        CreateMap<BuildComponentsDto, Dal.Entities.Build>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<BuildPeripheralsDto, Dal.Entities.Build>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());

        CreateMap<BuildFinishDto, Dal.Entities.Build>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}
