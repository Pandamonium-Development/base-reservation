using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of user's branch calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class UserBranchController(IServiceUserBranch serviceUserBranch) : ControllerBase
{
    /// <summary>
    /// Assign users to specific branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="usersBranchDto">List of users</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Branch/{branchId}/Users")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateUserBranchAsync(byte branchId, [FromBody] IEnumerable<RequestUserBranchDto> usersBranchDto)
    {
        ArgumentNullException.ThrowIfNull(usersBranchDto);
        var result = await serviceUserBranch.CreateUserBranchAsync(branchId, usersBranchDto);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Check if user is available to assign to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Branch/{id}/Branch/{branchId}/availability")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> IsAvailableAsync(short id, byte branchId)
    {
        var available = await serviceUserBranch.IsAvailableAsync(id, branchId);
        return StatusCode(StatusCodes.Status200OK, available);
    }
}