using AutoMapper;
using BeyondComputersNi.Api.ViewModels;
using BeyondComputersNi.Services.Interfaces;
using BeyondComputersNi.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComputerController(IComputerService computerService, IMapper mapper,
    IHttpContextAccessor httpContextAccessor) : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<ComputerViewModel>>> GetAllComputers()
    {
        var email = httpContextAccessor.HttpContext.User.GetEmail();
        return mapper.Map<List<ComputerViewModel>>(await computerService.GetAllComputers());
    }
}
