using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Build;
using BeyondComputersNi.Services.DataTransferObjects.Build;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BuildController(IBuildService buildService, IMapper mapper) : BaseController
{
    [HttpPost("create")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CreateBuild()
    {
        return OkOrError(await buildService.CreateBuild());
    }

    [HttpPut("components")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> AddComponents(BuildComponentsViewModel buildComponents)
    {
        if (!await buildService.BuildExists(buildComponents.Id))
            return NotFound("Your build does not exist, please create a new one.");

        return NoContentOrBadRequest(
            await buildService.AddComponents(mapper.Map<BuildComponentsDto>(buildComponents)),
            "Components could not be added, please try again.");
    }
}
