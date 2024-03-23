using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<User, UserDto>()
            .ReverseMap();
    }
}
