using AutoMapper;
using BeyondComputersNi.Api.ViewModels;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputerController(IComputerService computerService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ComputerViewModel>>> GetAllComputers()
    {
        return mapper.Map<List<ComputerViewModel>>(await computerService.GetAllComputers());
    }
}
