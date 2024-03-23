using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeyondComputersNi.Services.Services;
public class ComputerService(IBcniDbContext dbContext, IMapper mapper) : IComputerService
{
    public Task<List<ComputerDto>> GetAllComputers()
    {
        return mapper.ProjectTo<ComputerDto>(dbContext.Get<Computer>()).ToListAsync();
    }
}
