using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BeyondComputersNi.Api.Controllers;

public abstract class BaseController : ControllerBase
{

    protected ActionResult OkOrNotFound(bool successful, string errorMessage = "")
    {
        if (successful)
            return Ok();
        else
            return NotFound(errorMessage);
    }

    protected ActionResult OkOrNotFound(object? item, string errorMessage = "")
    {
        if (item == null || (item as ICollection)?.Count == 0)
            return NotFound(errorMessage);
        else
            return Ok(item);
    }

    protected ActionResult OkOrUnauthorized(bool successful, string errorMessage = "")
    {
        if (successful)
            return Ok();
        else
            return Unauthorized(errorMessage);
    }

    protected ActionResult OkOrUnauthorized(object? item, string errorMessage = "")
    {
        if (item == null || (item as ICollection)?.Count == 0)
            return Unauthorized(errorMessage);
        else
            return Ok(item);
    }

    protected ActionResult OkOrError(bool successful, string errorMessage = "")
    {
        if (successful)
            return Ok();
        else
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
    }

    protected ActionResult OkOrError(object? item, string errorMessage = "")
    {
        if (item == null || (item as ICollection)?.Count == 0)
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
        else
            return Ok(item);
    }

    protected ActionResult CreatedOrError(bool successful, string errorMessage = "")
    {
        if (successful)
            return Created();
        else
            return StatusCode(StatusCodes.Status500InternalServerError, errorMessage);
    }

    protected ActionResult NoContentOrBadRequest(bool successful, string errorMessage = "")
    {
        if (successful)
            return NoContent();
        else
            return BadRequest(errorMessage);
    }
}
