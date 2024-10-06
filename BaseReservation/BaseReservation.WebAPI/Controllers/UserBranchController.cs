using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}