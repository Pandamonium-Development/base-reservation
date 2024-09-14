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
[BaseReservationAuthorize(Rol.ADMINISTRADOR)]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class UsuarioController(IServiceUsuario serviceUsuario) : ControllerBase
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUsuarioDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllUsersAsync()
    {
        var users = await serviceUsuario.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, users);
    }

    /// <summary>
    /// Get list of all users by role
    /// </summary>
    /// <param name="rol">Role to look for</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/[controller]/ByRol/{rol}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseUsuarioDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllUsersByRolAsync(string rol)
    {
        var users = await serviceUsuario.ListAllAsync(rol);
        return StatusCode(StatusCodes.Status200OK, users);
    }

    /// <summary>
    /// Check if user is availably to assign to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/[controller]/{id}/Sucursal/{idSucursal}/libre-asignacion")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ValidarLibreAsignacionSucursalAsync(short id, byte idSucursal)
    {
        var available = await serviceUsuario.FreeAssignmentSucursalAsync(id, idSucursal);
        return StatusCode(StatusCodes.Status200OK, available);
    }
}