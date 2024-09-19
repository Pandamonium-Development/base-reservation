using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the DetalleFacturaController with the specified detail invoice service.
/// </summary>
/// <param name="serviceDetalleFactura">The service responsible for handling detail invoice operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class DetalleFacturaController(IServiceDetalleFactura serviceDetalleFactura) : ControllerBase
{
    /// <summary>
    /// Retrieves all detail invoices associated with a specific invoice.
    /// </summary>
    /// <param name="idFactura">The ID of the invoice.</param>
    /// <returns>A list of detail invoices for the specified invoice.</returns>
    [HttpGet("~/api/Factura/{idFactura}/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseDetalleFacturaDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByFacturaAsync(long idFactura)
    {
        var detailInvoice = await serviceDetalleFactura.ListAllByFacturaAsync(idFactura);
        return StatusCode(StatusCodes.Status200OK, detailInvoice);
    }

    /// <summary>
    /// Retrieves a specific detail invoice by its ID and associated invoice ID.
    /// </summary>
    /// <param name="idDetalleFactura">The ID of the detail invoice.</param>
    /// <returns>The detail invoice for the specified invoice and detail invoice IDs.</returns>
    /// <returns></returns>
    [HttpGet("~/api/Factura/{idFactura}/[controller]/{idDetalleFactura}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseDetalleFacturaDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long idDetalleFactura)
    {
        var detailInvoices = await serviceDetalleFactura.FindByIdAsync(idDetalleFactura);
        return StatusCode(StatusCodes.Status200OK, detailInvoices);
    }
}