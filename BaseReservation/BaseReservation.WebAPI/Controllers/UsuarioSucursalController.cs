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
public class UsuarioSucursalController(IServiceUsuarioSucursal serviceUsuarioSucursal) : ControllerBase
{
    /// <summary>
    /// Assign users to specific branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="usuarioSucursalDto">List of users</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Sucursal/{idSucursal}/Usuarios")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> AssignBranchUsers(byte idSucursal, [FromBody] IEnumerable<RequestUsuarioSucursalDto> usuarioSucursalDto)
    {
        ArgumentNullException.ThrowIfNull(usuarioSucursalDto);
        var result = await serviceUsuarioSucursal.CreateUsuarioSucursalAsync(idSucursal, usuarioSucursalDto);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}