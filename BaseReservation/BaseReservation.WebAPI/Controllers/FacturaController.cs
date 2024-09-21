using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified invoice service.
/// </summary>
/// <param name="serviceFactura">The service used for invoice operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class FacturaController(IServiceFactura serviceFactura) : ControllerBase
{
    /// <summary>
    /// Retrieves all invoices.
    /// </summary>
    /// <returns>A list of all invoices.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseFacturaDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var invoices = await serviceFactura.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, invoices);
    }

    /// <summary>
    /// Retrieves a specific invoice by its ID.
    /// </summary>
    /// <param name="idFactura">The ID of the invoice.</param>
    /// <returns>The details of the specified invoice.</returns>
    [HttpGet("{idFactura}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseFacturaDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long idFactura)
    {
        var invoice = await serviceFactura.FindByIdAsync(idFactura);
        return StatusCode(StatusCodes.Status200OK, invoice);
    }

    /// <summary>
    /// Creates a new invoice.
    /// </summary>
    /// <param name="factura">The invoice data to be created.</param>
    /// <returns>The created invoice.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseFacturaDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateFacturaAsync([FromBody] RequestFacturaDto factura)
    {
        ArgumentNullException.ThrowIfNull(factura);
        var result = await serviceFactura.CreateFacturaAsync(factura);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}