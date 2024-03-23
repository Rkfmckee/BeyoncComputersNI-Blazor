using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class ComputerDtoProfile : Profile
{
    public ComputerDtoProfile()
    {
        CreateMap<Computer, ComputerDto>();
    }
}
