using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified district service.
/// </summary>
/// <param name="serviceDistrict">The service used for district operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class DistrictController(IServiceDistrict serviceDistrict) : ControllerBase
{
    /// <summary>
    /// Retrieves all districts associated with a specific canton.
    /// </summary>
    /// <param name="cantonId">The ID of the canton.</param>
    /// <returns>A list of districts for the specified canton.</returns>
    [HttpGet("~/api/Canton/{cantonId}/District")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseDistrictDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByCantonAsync(byte cantonId)
    {
        var districts = await serviceDistrict.ListAllByCantonAsync(cantonId);
        return StatusCode(StatusCodes.Status200OK, districts);
    }

    /// <summary>
    /// Retrieves details of a specific district by its ID.
    /// </summary>
    /// <param name="id">The ID of the district.</param>
    /// <returns>The details of the specified district.</returns>
    [HttpGet("{idDistrict}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDistrictDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte id)
    {
        var districts = await serviceDistrict.FindByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, districts);
    }
}