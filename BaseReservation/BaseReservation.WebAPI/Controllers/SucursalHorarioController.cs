using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class SucursalHorarioController(IServiceSucursalHorario serviceSucursalHorario) : ControllerBase
{
    /// <summary>
    /// Get branch schedule with specific id
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idSucursalHorario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalHorarioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(short idSucursalHorario)
    {
        var schedule = await serviceSucursalHorario.FindByIdAsync(idSucursalHorario);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Get schedules by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Sucursal/{idSucursal}/Horario")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalHorarioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllBySucursalAsync(byte idSucursal)
    {
        var schedules = await serviceSucursalHorario.ListAllBySucursalAsync(idSucursal);
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

    /// <summary>
    /// Assign schedules to a branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursalHorarios">List of schedules</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Sucursal/{idSucursal}/Horario")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateSucursalHorarioAsync(byte idSucursal, [FromBody] IEnumerable<RequestSucursalHorarioDto> sucursalHorarios)
    {
        ArgumentNullException.ThrowIfNull(sucursalHorarios);
        var result = await serviceSucursalHorario.CreateSucursalHorarioAsync(idSucursal, sucursalHorarios);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}