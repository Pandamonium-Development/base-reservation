using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
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
public class SucursalController(IServiceSucursal serviceSucursal) : ControllerBase
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSucursalDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var branches = await serviceSucursal.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, branches);
    }

    /// <summary>
    /// Get list of all branches by role from user logged in 
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet("ByRol")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseSucursalDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByRolAsync()
    {
        var branch = await serviceSucursal.ListAllByRolAsync();
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idSucursal}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte idSucursal)
    {
        var branch = await serviceSucursal.FindByIdAsync(idSucursal);
        return StatusCode(StatusCodes.Status200OK, branch);
    }

    /// <summary>
    /// Create new branch
    /// </summary>
    /// <param name="sucursal">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateSucursalAsync([FromBody] RequestSucursalDto sucursal)
    {
        ArgumentNullException.ThrowIfNull(sucursal);
        var result = await serviceSucursal.CreateSucursalAsync(sucursal);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursal">Branch request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idSucursal}")]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateSucursalAsync(byte idSucursal, [FromBody] RequestSucursalDto sucursal)
    {
        ArgumentNullException.ThrowIfNull(sucursal);
        var result = await serviceSucursal.UpdateSucursalAsync(idSucursal, sucursal);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}