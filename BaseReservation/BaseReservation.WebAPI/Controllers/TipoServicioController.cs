using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of service type calls
/// </summary>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class TipoServicioController(IServiceTipoServicio serviceTipoServicio) : ControllerBase
{
    /// <summary>
    /// Get list of all service types
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseTipoServicioDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllTipoServiciosAsync()
    {
        var serviceTypes = await serviceTipoServicio.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, serviceTypes);
    }
}