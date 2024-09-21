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
/// <param name="serviceDistrito">The service used for district operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class DistritoController(IServiceDistrito serviceDistrito) : ControllerBase
{
    /// <summary>
    /// Retrieves all districts associated with a specific canton.
    /// </summary>
    /// <param name="idCanton">The ID of the canton.</param>
    /// <returns>A list of districts for the specified canton.</returns>
    [HttpGet("~/api/Canton/{idCanton}/Distrito")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseDistritoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByCantonAsync(byte idCanton)
    {
        var districts = await serviceDistrito.ListAllByCantonAsync(idCanton);
        return StatusCode(StatusCodes.Status200OK, districts);
    }

    /// <summary>
    /// Retrieves details of a specific district by its ID.
    /// </summary>
    /// <param name="id">The ID of the district.</param>
    /// <returns>The details of the specified district.</returns>
    [HttpGet("{idDistrito}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDistritoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte id)
    {
        var districts = await serviceDistrito.FindByIdAsync(id);
        return StatusCode(StatusCodes.Status200OK, districts);
    }
}