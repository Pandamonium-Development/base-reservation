using Asp.Versioning;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of province calls
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/[controller]")]
public class ProvinciaController(IServiceProvincia serviceProvincia) : ControllerBase
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseProvinciaDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllProvinciasAsync()
    {
        var provincias = await serviceProvincia.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, provincias);
    }

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="idProvincia">Province id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idProvincia}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProvinciaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetProvinciaByIdAsync(byte idProvincia)
    {
        var provincia = await serviceProvincia.FindByIdAsync(idProvincia);
        return StatusCode(StatusCodes.Status200OK, provincia);
    }
}