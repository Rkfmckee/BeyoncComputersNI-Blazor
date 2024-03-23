using AutoMapper;
using BeyondComputersNi.Api.ViewModels;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputerController(IComputerService computerService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<ComputerViewModel>>> GetAllComputers()
    {
        return mapper.Map<List<ComputerViewModel>>(await computerService.GetAllComputers());
    }
}
