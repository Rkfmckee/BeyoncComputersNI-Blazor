using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildController : ControllerBase
{
    [HttpPost("create")]
    public async Task<ActionResult<int>> CreateBuild()
    {
        return 1;
    }
}
