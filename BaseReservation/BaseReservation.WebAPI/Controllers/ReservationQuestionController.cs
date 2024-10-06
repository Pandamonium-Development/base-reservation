using Asp.Versioning;
using BaseReservation.Application.Common;
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
public class ReservationQuestionController(IServiceReservationQuestion serviceReservationQuestion) : ControllerBase
{
    /// <summary>
    /// Get list of all reservation's questions
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseReservationQuestionDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var reservationQuestions = await serviceReservationQuestion.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, reservationQuestions);
    }

    /// <summary>
    /// Get reservation question with specific id
    /// </summary>
    /// <param name="reservationQuestionId">Reservation question Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{reservationQuestionId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationQuestionDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(int reservationQuestionId)
    {
        var reservationQuestion = await serviceReservationQuestion.FindByIdAsync(reservationQuestionId);
        return StatusCode(StatusCodes.Status200OK, reservationQuestion);
    }
}