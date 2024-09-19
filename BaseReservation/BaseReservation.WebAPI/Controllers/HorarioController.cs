using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified schedule service.
/// </summary>
/// <param name="serviceHorario">The service used for schedule operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class HorarioController(IServiceHorario serviceHorario) : ControllerBase
{
    /// <summary>
    /// Retrieves all schedules.
    /// </summary>
    /// <returns>A collection of all schedules.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseHorarioDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllHorariosAsync()
    {
        var schedules = await serviceHorario.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, schedules);
    }

    /// <summary>
    /// Retrieves a specific schedule by its ID.
    /// </summary>
    /// <param name="idHorario">The ID of the schedule.</param>
    /// <returns>The details of the specified schedule.</returns>
    [HttpGet("{idHorario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseHorarioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetHorarioByIdAsync(short idHorario)
    {
        var schedule = await serviceHorario.FindByIdAsync(idHorario);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Creates a new schedule.
    /// </summary>
    /// <param name="horario">The schedule data to be created.</param>
    /// <returns>The created schedule.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseHorarioDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateHorarioAsync([FromBody] RequestHorarioDto horario)
    {
        ArgumentNullException.ThrowIfNull(horario);
        var schedule = await serviceHorario.CreateHorarioAsync(horario);
        return StatusCode(StatusCodes.Status201Created, schedule);
    }

    /// <summary>
    /// Updates an existing schedule by its ID.
    /// </summary>
    /// <param name="idHorario">The ID of the schedule to update.</param>
    /// <param name="horario">The updated schedule data.</param>
    /// <returns>The updated schedule.</returns>
    [HttpPut("{idHorario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseHorarioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateHorarioAsync(short idHorario, [FromBody] RequestHorarioDto horario)
    {
        ArgumentNullException.ThrowIfNull(horario);
        var schedule = await serviceHorario.UpdateHorarioAsync(idHorario, horario);
        return StatusCode(StatusCodes.Status200OK, schedule);
    }

    /// <summary>
    /// Deletes a specific schedule by its ID.
    /// </summary>
    /// <param name="idHorario">The ID of the schedule to delete.</param>
    /// <returns>IActionResult</returns>
    [HttpDelete("~/api/[controller]/{idHorario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteHorario(short idHorario)
    {
        var result = await serviceHorario.DeleteHorarioAsync(idHorario);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}