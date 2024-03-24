using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Authentication;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpPost("Register")]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (await userService.UserExistsAsync(registerViewModel.Email))
            return Conflict("User already exists.");

        var successful = await userService.AddUserAsync(mapper.Map<UserDto>(registerViewModel));

        if (successful)
            return Created();
        else
            return StatusCode(StatusCodes.Status500InternalServerError, "User could not be created, please try again.");
    }
}
