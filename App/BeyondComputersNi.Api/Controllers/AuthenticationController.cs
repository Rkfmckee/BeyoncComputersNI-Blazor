using AutoMapper;
using BeyondComputersNi.Api.ViewModels.Authentication;
using BeyondComputersNi.Services.DataTransferObjects;
using BeyondComputersNi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeyondComputersNi.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IUserService userService, IAuthenticationService authenticationService, IMapper mapper) : BaseController
{
    [HttpPost("Register")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (await userService.UserExistsAsync(registerViewModel.Email))
            return Conflict("User already exists.");

        return CreatedOrError(await userService.AddUserAsync(mapper.Map<UserDto>(registerViewModel)),
            "User could not be created, please try again.");
    }

    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Login(LoginViewModel loginViewModel)
    {
        var user = await userService.GetUserAsync(loginViewModel.Email);

        if (user == null || !userService.PasswordIsCorrect(user, loginViewModel.Password))
            return Unauthorized("Invalid credentials.");

        return OkOrUnauthorized(
            mapper.Map<AuthenticationViewModel>(await authenticationService.AuthenticateAsync(user.Email)),
            "Login failed, please try again.");
    }

    [HttpPost("Refresh")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Refresh(RefreshViewModel refreshViewModel)
    {
        return OkOrUnauthorized(
            mapper.Map<AuthenticationViewModel>(await authenticationService.RefreshAsync(mapper.Map<RefreshDto>(refreshViewModel))),
            "Refresh failed, please try again.");
    }

    [HttpDelete("Revoke")]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Revoke()
    {
        return OkOrError(await authenticationService.RevokeAsync());
    }
}
