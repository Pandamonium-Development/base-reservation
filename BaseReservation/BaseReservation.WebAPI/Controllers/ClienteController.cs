using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Implementations;
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
    /// Retrieves a list of all customers.
    /// </summary>
    /// <returns>A list of all clients.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseClienteDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var customers = await serviceCliente.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, customers);
    }

    /// <summary>
    /// Deletes a customer by its ID.
    /// </summary>
    /// <param name="idCostumer">The ID of the holiday to delete.</param>
    /// <returns>The deleted holiday.</returns>
    [HttpDelete("{idCostumer}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseClienteDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteCustomerAsync(byte idCostumer)
    {
        var customer = await serviceCliente.DeleteCustomerAsync(idCostumer);
        return StatusCode(StatusCodes.Status200OK, customer);
    }

    /// <summary>
    /// Retrieves a specific customer by its ID.
    /// </summary>
    /// <param name="idCostumer">The ID of the customer.</param>
    /// <returns>The details of the specified customer.</returns>
    [HttpGet("{idCostumer}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseClienteDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetCustomerByIdAsync(short idCostumer)
    {
        var customer = await serviceCliente.FindByIdAsync(idCostumer);
        return StatusCode(StatusCodes.Status200OK, customer);
    }
}