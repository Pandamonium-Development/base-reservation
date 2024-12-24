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
public class ReservationDetailController(IServiceReservationDetail serviceReservationDetail) : ControllerBase
{
    /// <summary>
    /// Get reservation detail with specific id
    /// </summary>
    /// <param name="reservationDetailId">Reservation detail Id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{reservationDetailId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(int reservationDetailId)
    {
        var detailReservation = await serviceReservationDetail.FindByIdAsync(reservationDetailId);
        return StatusCode(StatusCodes.Status200OK, detailReservation);
    }

    /// <summary>
    /// Get list of all reservation's details by branch
    /// </summary>
    /// <param name="id">Branch id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("~/api/Reservation/{id}/Detail")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseReservationDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByReservationAsync(int id)
    {
        var details = await serviceReservationDetail.ListAllByReservationAsync(id);
        return StatusCode(StatusCodes.Status200OK, details);
    }

    /// <summary>
    /// Create details reservation
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="detailsReservation">List of details to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost("~/api/Reservation/{id}/Detail")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateReservationDetailAsync(int branchId, [FromBody] IEnumerable<RequestReservationDetailDto> detailsReservation)
    {
        ArgumentNullException.ThrowIfNull(detailsReservation);
        var result = await serviceReservationDetail.CreateReservationDetailAsync(branchId, detailsReservation);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}