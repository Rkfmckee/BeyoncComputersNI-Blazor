using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BeyondComputersNi.Api.Controllers;

public abstract class BaseController : ControllerBase
{
    protected ActionResult OkOrError(bool successful, string okMessage = "", string errorMessage = "")
    {
        if (successful)
            return Ok(okMessage);
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
}
