using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ServicioController(IServiceServicio serviceServicio) : ControllerBase
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseServicioDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllServiciosAsync()
    {
        var servicios = await serviceServicio.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, servicios);
    }

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="idServicio">Service id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idServicio}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetServicioByIdAsync(byte idServicio)
    {
        var servicio = await serviceServicio.FindByIdAsync(idServicio);
        return StatusCode(StatusCodes.Status200OK, servicio);
    }

    /// <summary>
    /// Create new service
    /// </summary>
    /// <param name="servicio">Service request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateServicioAsync([FromBody] RequestServicioDto servicio)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(servicio);
        var result = await serviceServicio.CreateServicioAsync(servicio);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing service
    /// </summary>
    /// <param name="idServicio">Service id</param>
    /// <param name="servicio">Service request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idServicio}")]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateServicioAsync(byte idServicio, [FromBody] RequestServicioDto servicio)
    {
        //retorna una excepçión is es nulo
        ArgumentNullException.ThrowIfNull(servicio);
        var result = await serviceServicio.UpdateServicioAsync(idServicio, servicio);
        return StatusCode(StatusCodes.Status200OK, result);
    }
}