using Asp.Versioning;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of reservation question calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ReservaPreguntaController(IServiceReservaPregunta serviceReservaPregunta) : ControllerBase
{
    /// <summary>
    /// Get list of all reservation's questions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseReservaPreguntaDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllReservasPreguntasAsync()
    {
        var questions = await serviceReservaPregunta.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, questions);
    }

    /// <summary>
    /// Get reservation question with specific id
    /// </summary>
    /// <param name="idReservaPregunta">Reservation question Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idReservaPregunta}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservaPreguntaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetReservaPreguntaByIdAsync(int idReservaPregunta)
    {
        var reservationQuestions = await serviceReservaPregunta.FindByIdAsync(idReservaPregunta);
        return StatusCode(StatusCodes.Status200OK, reservationQuestions);
    }
}