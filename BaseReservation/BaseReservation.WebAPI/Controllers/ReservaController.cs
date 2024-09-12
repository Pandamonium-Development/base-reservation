using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.Application.Comunes;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservations calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ReservaController(IServiceReserva serviceReserva) : ControllerBase
{
    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseReservaDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllReservasAsync()
    {
        var reservations = await serviceReserva.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, reservations);
    }

    /// <summary>
    /// Get list of all reservations by branch
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idReserva}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservaDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetReservaByIdAsync(int idReserva)
    {
        var reservation = await serviceReserva.FindByIdAsync(idReserva);
        return StatusCode(StatusCodes.Status200OK, reservation);
    }

    /// <summary>
    /// Get list of all reservations by branch with filter option with start date and end date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="fechaInicio">Start date</param>
    /// <param name="fechaFin">End date</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Sucursal/{idSucursal}/calendario")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ICollection<ResponseAgendaCalendarioReservaDto>>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllReservasAsync(byte idSucursal, [FromQuery] DateOnly? fechaInicio, [FromQuery] DateOnly? fechaFin)
    {
        var reservations = await serviceReserva.ListAllBySucursalAsync(idSucursal, fechaInicio, fechaFin);
        return StatusCode(StatusCodes.Status200OK, reservations);
    }

    /// <summary>
    /// Get availablity for a branch in specific date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="dia">Date to filter</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Sucursal/{idSucursal}/Disponibilidad-Dia/{dia}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<TimeOnly>))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ScheduleAvailabilityBySucursalAsync(byte idSucursal, DateTime dia)
    {
        var availablesHours = await serviceReserva.ScheduleAvailabilityBySucursalAsync(idSucursal, new DateOnly(dia.Year, dia.Month, dia.Day));
        return StatusCode(StatusCodes.Status200OK, availablesHours);
    }

    /// <summary>
    /// Create a new reservation
    /// </summary>
    /// <param name="reserva">Reservation request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseReservaDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateReservaAsync([FromBody] RequestReservaDto reserva)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(reserva);
        var result = await serviceReserva.CreateReservaAsync(reserva);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update an existing reservation
    /// </summary>
    /// <param name="idServicio">Service id</param>
    /// <param name="reserva">Reservation request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idReserva}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateReservaAsync(int idServicio, [FromBody] RequestReservaDto reserva)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(reserva);
        var result = await serviceReserva.UpdateReservaAsync(idServicio, reserva);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}