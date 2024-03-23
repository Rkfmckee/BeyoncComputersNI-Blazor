using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Authentication;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<RegisterViewModel, UserDto>();
    }
}
