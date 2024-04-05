using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Services.DataTransferObjects;

namespace BeyondComputersNi.Services.MappingProfiles;

public class ComputerProfiles : Profile
{
    public ComputerProfiles()
    {
        CreateMap<Computer, ComputerDto>();
    }
}
