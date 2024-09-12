using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified client service.
/// </summary>
/// <param name="serviceCliente">The service used for client operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ClienteController(IServiceCliente serviceCliente) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all clients.
    /// </summary>
    /// <returns>A list of all clients.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseClienteDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> Index()
    {
        var customers = await serviceCliente.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, customers);
    }
}