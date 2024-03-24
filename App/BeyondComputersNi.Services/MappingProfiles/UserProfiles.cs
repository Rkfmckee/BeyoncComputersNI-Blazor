using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;
using BC = BCrypt.Net.BCrypt;

namespace BeyondComputersNi.Services.MappingProfiles;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, 
                opt => opt.MapFrom(src => BC.HashPassword(src.Password)))

            .ReverseMap()
                .ForMember(dest => dest.Password,
                opt => opt.Ignore());
    }
}
