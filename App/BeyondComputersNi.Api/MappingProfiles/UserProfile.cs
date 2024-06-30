using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Authentication;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<RegisterViewModel, UserDto>();
    }
}
