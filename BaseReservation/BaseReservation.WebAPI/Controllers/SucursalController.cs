using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Implementations;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class SucursalController(IServiceSucursal serviceBranch) : ControllerBase
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSucursalDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllBranchesAsync()
    {
        var branches = await serviceBranch.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, branches);
    }

    /// <summary>
    /// Get list of all branches by role from user logged in 
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet("ByRol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSucursalDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllBranchesByRolAsync()
    {
        var branch = await serviceBranch.ListAllByRolAsync();
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="idBranch">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idBranch}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetBranchlByIdAsync(byte idBranch)
    {
        var branch = await serviceBranch.FindByIdAsync(idBranch);
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Create new branch
    /// </summary>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateSucursalAsync([FromBody] RequestSucursalDto branch)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.CreateBranchAsync(branch);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing branch
    /// </summary>
    /// <param name="idBranch">Branch id</param>
    /// <param name="branch">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idSucursal}")]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateBranchAsync(byte idBranch, [FromBody] RequestSucursalDto branch)
    {
        ArgumentNullException.ThrowIfNull(branch);
        var result = await serviceBranch.UpdateBranchAsync(idBranch, branch);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a branch by its ID.
    /// </summary>
    /// <param name="idBranch">The ID of the branch to delete.</param>
    /// <returns>The deleted branch.</returns>
    [HttpDelete("{idBranch}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteBranchAsync(byte idBranch)
    {
        var branch = await serviceBranch.DeleteBranchAsync(idBranch);
        return StatusCode(StatusCodes.Status200OK, branch);
    }
}