using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of branch's schedule block calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class SucursalHorarioBloqueoController(IServiceSucursalHorarioBloqueo serviceBloqueo) : ControllerBase
{
    /// <summary>
    /// Create new block of branch's schedule
    /// </summary>
    /// <param name="bloqueo">Block request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseSucursalHorarioBloqueoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateSucursalHorarioBloqueoAsync([FromBody] RequestSucursalHorarioBloqueoDto bloqueo)
    {
        ArgumentNullException.ThrowIfNull(bloqueo);
        var result = await serviceBloqueo.CreateSucursalHorarioBloqueoAsync(bloqueo);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Assign new blocks of branch's schedule
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <param name="bloqueos">List Block request model to be assign</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/SucursalHorario/{idSucursalHorario}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseSucursalHorarioBloqueoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, [FromBody] IEnumerable<RequestSucursalHorarioBloqueoDto> bloqueos)
    {
        ArgumentNullException.ThrowIfNull(bloqueos);
        var result = await serviceBloqueo.CreateSucursalHorarioBloqueoAsync(idSucursalHorario, bloqueos);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing block of branch's schedule
    /// </summary>
    /// <param name="idSucursalHorarioBloqueo">Block branch's schedule id</param>
    /// <param name="bloqueo">Block request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idSucursalHorarioBloqueo}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseSucursalHorarioBloqueoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateSucursalHorarioBloqueoAsync(long idSucursalHorarioBloqueo, [FromBody] RequestSucursalHorarioBloqueoDto bloqueo)
    {
        ArgumentNullException.ThrowIfNull(bloqueo);
        var result = await serviceBloqueo.UpdateSucursalHorarioBloqueoAsync(idSucursalHorarioBloqueo, bloqueo);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}