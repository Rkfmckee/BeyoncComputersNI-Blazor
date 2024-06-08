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
    [HttpGet("Exists/{id}")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> BuildExists(int id)
    {
        return Ok(await buildService.BuildExistsAsync(id));
    }

    [HttpGet("Number/{id}")]
    [ProducesResponseType(typeof(BuildNumberViewModel), StatusCodes.Status200OK)]
    public async Task<ActionResult<BuildNumberViewModel>> GetBuildNumber(int id)
    {
        return Ok(new BuildNumberViewModel(await buildService.GetBuildNumberAsync(id)));
    }

    [HttpPost("Create")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> CreateBuild()
    {
        return OkOrError(await buildService.CreateBuildAsync());
    }

    [HttpGet("Components/{componentName}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<string>>> GetComponents(string componentName, [FromQuery] string? value = null)
    {
        return Ok(await buildService.GetComponentsAsync(componentName, value));
    }

    [HttpPut("Components")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> AddComponents(BuildComponentsViewModel buildComponents)
    {
        if (!await buildService.BuildExistsAsync(buildComponents.Id))
            return NotFound("Your build does not exist, please create a new one.");

        return NoContentOrBadRequest(
            await buildService.AddComponentsAsync(mapper.Map<BuildComponentsDto>(buildComponents)),
            "Components could not be added, please try again.");
    }

    [HttpPut("Peripherals")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> AddPeripherals(BuildPeripheralsViewModel buildPeripherals)
    {
        if (!await buildService.BuildExistsAsync(buildPeripherals.Id))
            return NotFound("Your build does not exist, please create a new one.");

        return NoContentOrBadRequest(
            await buildService.AddPeripheralsAsync(mapper.Map<BuildPeripheralsDto>(buildPeripherals)),
            "Peripherals could not be added, please try again.");
    }

    [HttpPut("Finish")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> FinishBuild(BuildFinishViewModel buildFinish)
    {
        if (!await buildService.BuildExistsAsync(buildFinish.Id))
            return NotFound("Your build does not exist, please create a new one.");

        return NoContentOrBadRequest(
            await buildService.FinishBuildAsync(mapper.Map<BuildFinishDto>(buildFinish)),
            "Build could not be finished, please try again.");
    }
}
