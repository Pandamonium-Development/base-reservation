using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Implementations;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller in charge of service calls
/// </summary>
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
    public async Task<IActionResult> ListAllAsync()
    {
        var services = await serviceServicio.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, services);
    }

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="idService">Service id</param>
    /// <returns>IActionResult</returns>
    [HttpGet("{idServicio}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte idServicio)
    {
        var service = await serviceServicio.FindByIdAsync(idService);
        return StatusCode(StatusCodes.Status200OK, service);
    }

    /// <summary>
    /// Create new service
    /// </summary>
    /// <param name="service">Service request model to be added</param>
    /// <returns>IActionResult</returns>
    [HttpPost]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateServiceAsync([FromBody] RequestServicioDto service)
    {
        ArgumentNullException.ThrowIfNull(service);
        var result = await serviceServicio.CreateServiceAsync(service);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    /// <summary>
    /// Update existing service
    /// </summary>
    /// <param name="idService">Service id</param>
    /// <param name="service">Service request model to be updated</param>
    /// <returns>IActionResult</returns>
    [HttpPut("{idService}")]
    [BaseReservationAuthorize(Rol.ADMINISTRADOR)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateServiceAsync(byte idService, [FromBody] RequestServicioDto service)
    {
        ArgumentNullException.ThrowIfNull(service);
        var result = await serviceServicio.UpdateServiceAsync(idService, service);
        return StatusCode(StatusCodes.Status200OK, result);
    }

    /// <summary>
    /// Deletes a service by its ID.
    /// </summary>
    /// <param name="idService">The ID of the service to delete.</param>
    /// <returns>The deleted service.</returns>
    [HttpDelete("{idService}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseServicioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteServiceAsync(byte idService)
    {
        var service = await serviceServicio.DeleteServiceAsync(idService);
        return StatusCode(StatusCodes.Status200OK, service);
    }
}