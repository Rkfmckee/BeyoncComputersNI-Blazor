using AutoMapper;
using BeyondComputersNi.Dal.Entities;
using BeyondComputersNi.Dal.Interfaces;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Web.Http;

namespace BeyondComputersNi.Services.Services;
public class ComputerService(IRepository<Computer> computerRepo, IMapper mapper) : IComputerService
{
    [Authorize]
    public Task<List<ComputerDto>> GetAllComputers()
    {
        return mapper.ProjectTo<ComputerDto>(computerRepo.Get()).ToListAsync();
    }
}
