using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Application.Dtos.Response.Enums;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of users calls
/// </summary>
[ApiController]
[BaseReservationAuthorize(RoleApplication.ADMINISTRADOR)]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class UserController(IServiceUser serviceUser) : ControllerBase
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUserDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var users = await serviceUser.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, users);
    }

    /// <summary>
    /// Get list of all users by role
    /// </summary>
    /// <param name="role">Role to look for</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/[controller]/ByRol/{rol}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUserDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync(string role)
    {
        var users = await serviceUser.ListAllAsync(role);
        return StatusCode(StatusCodes.Status200OK, users);
    }
}