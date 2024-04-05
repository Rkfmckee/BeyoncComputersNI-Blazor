using AutoMapper;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Shared.ViewModels.Authentication;

namespace BeyondComputersNi.Services.MappingProfiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<RegisterViewModel, UserDto>();
    }
}
