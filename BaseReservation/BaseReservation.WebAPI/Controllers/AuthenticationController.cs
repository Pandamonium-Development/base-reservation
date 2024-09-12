using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs.Authentication;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the AuthenticationController with the specified service identity.
/// </summary>
/// <param name="serviceIdentity">The service identity used for authentication operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class AuthenticationController(IServiceIdentity serviceIdentity) : ControllerBase
{
    /// <summary>
    /// Logs in a user using the provided login model.
    /// </summary>
    /// <param name="loginModel">The login credentials.</param>
    /// <returns>An action result with authentication details or an error.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> LoginAsync([FromBody] RequestUserLoginDto loginModel)
    {
        var login = await serviceIdentity.LoginAsync(loginModel);
        return StatusCode(StatusCodes.Status200OK, login);
    }

    /// <summary>
    /// Refreshes the authentication token using the provided token model.
    /// </summary>
    /// <param name="request">The token refresh request.</param>
    /// <returns>An action result with the new authentication token or an error.</returns>
    [Route("refresh")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthenticationResult))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> Refresh([FromBody] TokenModel request)
    {
        var refreshToken = await serviceIdentity.RefreshTokenAsync(request);
        return StatusCode(StatusCodes.Status200OK, refreshToken);
    }
}