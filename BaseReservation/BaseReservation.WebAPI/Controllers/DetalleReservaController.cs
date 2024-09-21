using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservation detail calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class DetalleReservaController(IServiceDetalleReserva serviceDetalleReserva) : ControllerBase
{
    /// <summary>
    /// Get reservation detail with specific id
    /// </summary>
    /// <param name="idDetalleReserva">Reservation detail Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idDetalleReserva}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDetalleReservaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(int idDetalleReserva)
    {
        var detailReservation = await serviceDetalleReserva.FindByIdAsync(idDetalleReserva);
        return StatusCode(StatusCodes.Status200OK, detailReservation);
    }

    /// <summary>
    /// Get list of all reservation's details by branch
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Reserva/{idReserva}/Detalle")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDetalleReservaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByReservaAsync(int idReserva)
    {
        var details = await serviceDetalleReserva.ListAllByReservaAsync(idReserva);
        return StatusCode(StatusCodes.Status200OK, details);
    }

    /// <summary>
    /// Create details reservation
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <param name="detalleReserva">List of details to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Reserva/{idReserva}/Detalle")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateDetalleReservaAsync(int idReserva, [FromBody] IEnumerable<RequestDetalleReservaDto> detalleReserva)
    {
        ArgumentNullException.ThrowIfNull(detalleReserva);
        var result = await serviceDetalleReserva.CreateDetalleReservaAsync(idReserva, detalleReserva);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}