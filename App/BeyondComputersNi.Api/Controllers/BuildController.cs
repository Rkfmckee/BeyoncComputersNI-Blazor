using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildController(IBuildService buildService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<int>> CreateBuild()
    {
        return await buildService.CreateBuild();
    }
}
