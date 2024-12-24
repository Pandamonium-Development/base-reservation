using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of users calls
/// </summary>
[ApiController]
[BaseReservationAuthorize(Role.ADMINISTRADOR)]
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

    /// <summary>
    /// Check if user is availably to assign to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/[controller]/{id}/Branch/{branchId}/availability")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> IsAvailableAsync(short id, byte branchId)
    {
        var available = await serviceUser.IsAvailableAsync(id, branchId);
        return StatusCode(StatusCodes.Status200OK, available);
    }
}