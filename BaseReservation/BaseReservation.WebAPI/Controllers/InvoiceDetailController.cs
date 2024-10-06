using Asp.Versioning;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the InvoiceDetailController with the specified detail invoice service.
/// </summary>
/// <param name="serviceInvoiceDetail">The service responsible for handling detail invoice operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class InvoiceDetailController(IServiceInvoiceDetail serviceInvoiceDetail) : ControllerBase
{
    /// <summary>
    /// Retrieves all detail invoices associated with a specific invoice.
    /// </summary>
    /// <param name="invoiceId">The ID of the invoice.</param>
    /// <returns>A list of detail invoices for the specified invoice.</returns>
    [HttpGet("~/api/Invoice/{invoiceId}/[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseInvoiceDetailDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByInvoiceAsync(long invoiceId)
    {
        var detailsInvoice = await serviceInvoiceDetail.ListAllByInvoiceAsync(invoiceId);
        return StatusCode(StatusCodes.Status200OK, detailsInvoice);
    }

    /// <summary>
    /// Retrieves a specific detail invoice by its ID and associated invoice ID.
    /// </summary>
    /// <param name="invoiceDetailId">The ID of the detail invoice.</param>
    /// <returns>The detail invoice for the specified invoice and detail invoice IDs.</returns>
    /// <returns></returns>
    [HttpGet("~/api/Invoice/{invoiceId}/[controller]/{invoiceDetailId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInvoiceDetailDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long invoiceDetailId)
    {
        var detailInvoice = await serviceInvoiceDetail.FindByIdAsync(invoiceDetailId);
        return StatusCode(StatusCodes.Status200OK, detailInvoice);
    }
}