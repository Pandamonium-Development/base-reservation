using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the CantonController with the specified service canton.
/// </summary>
/// <param name="serviceCanton">The service used for canton operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class CantonController(IServiceCanton serviceCanton) : ControllerBase
{
    /// <summary>
    /// Retrieves all cantons associated with a specific province.
    /// </summary>
    /// <param name="idProvincia">The ID of the province.</param>
    /// <returns>A list of cantons within the specified province.</returns>
    [HttpGet("~/api/Provincia/{idProvincia}/Canton")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseCantonDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByByProvinciaAsync(byte idProvincia)
    {
        var cantons = await serviceCanton.ListAllByProvinciaAsync(idProvincia);
        return StatusCode(StatusCodes.Status200OK, cantons);
    }

    /// <summary>
    /// Retrieves details of a specific canton by its ID.
    /// </summary>
    /// <param name="id">The ID of the canton.</param>
    /// <returns>The details of the specified canton.</returns>
    [HttpGet("{idCanton}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCantonDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte id)
    {
        var cantons = await serviceCanton.FindByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, cantons);
    }
}