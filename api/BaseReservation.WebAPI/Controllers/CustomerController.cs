using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified client service.
/// </summary>
/// <param name="serviceCustomer">The service used for client operations.</param>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class CustomerController(IServiceCustomer serviceCustomer) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of all customers.
    /// </summary>
    /// <returns>A list of all clients.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseCustomerDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var customers = await serviceCustomer.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, customers);
    }

    /// <summary>
    /// Deletes a customer by its ID.
    /// </summary>
    /// <param name="customerId">The ID of the holiday to delete.</param>
    /// <returns>The deleted holiday.</returns>
    [HttpDelete("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteCustomerAsync(byte customerId)
    {
        var customer = await serviceCustomer.DeleteCustomerAsync(customerId);
        return StatusCode(StatusCodes.Status200OK, customer);
    }

    /// <summary>
    /// Retrieves a specific customer by its ID.
    /// </summary>
    /// <param name="customerId">The ID of the customer.</param>
    /// <returns>The details of the specified customer.</returns>
    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseCustomerDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetCustomerByIdAsync(short customerId)
    {
        var customer = await serviceCustomer.FindByIdAsync(customerId);
        return StatusCode(StatusCodes.Status200OK, customer);
    }
}