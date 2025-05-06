using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.Dtos.Request;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Application.Dtos.Response.Enums;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class BranchController(IServiceBranch serviceBranch) : ControllerBase
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseBranchDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var branches = await serviceBranch.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, branches);
    }

    /// <summary>
    /// Get list of all branches by role from user logged in 
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet("ByRol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseBranchDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByRoleAsync()
    {
        var branch = await serviceBranch.ListAllByRoleAsync();
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte branchId)
    {
        var branch = await serviceBranch.FindByIdAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Create new branch
    /// </summary>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateBranchAsync([FromBody] RequestBranchDto branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.CreateBranchAsync(branch);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{branchId}")]
    [BaseReservationAuthorize(RoleApplication.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBranchDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateBranchAsync(byte branchId, [FromBody] RequestBranchDto branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.UpdateBranchAsync(branchId, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a branch by its ID.
    /// </summary>
    /// <param name="branchId">The ID of the branch to delete.</param>
    /// <returns>The deleted branch.</returns>
    [HttpDelete("{branchId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteBranchAsync(byte branchId)
    {
        var branch = await serviceBranch.DeleteBranchAsync(branchId);
        return StatusCode(StatusCodes.Status200OK, branch);
    }
}