using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BaseReservation.WebAPI.Configuration;
using BaseReservation.Application.Dtos.Request;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Initializes a new instance of the controller with the specified tax service.
/// </summary>
/// <param name="serviceTax">The service used for tax operations.</param>
[ApiController]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class TaxController(IServiceTax serviceTax) : ControllerBase
{
    /// <summary>
    /// Retrieves all taxes.
    /// </summary>
    /// <returns>A collection of all taxes.</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ICollection<ResponseTaxDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var taxes = await serviceTax.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, taxes);
    }

    /// <summary>
    /// Get the exact tax by id.
    /// </summary>
    /// <param name="taxId">Id to look for</param>
    /// <returns>Exact tax</returns>
    [HttpGet("{taxId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTaxDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long taxId)
    {
        var tax = await serviceTax.FindByIdAsync(taxId);
        return StatusCode(StatusCodes.Status200OK, tax);
    }

    /// <summary>
    /// Creates a new tax.
    /// </summary>
    /// <param name="requestTaxDto">Tax request model to be added</param>
    /// <returns>Created tax</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseTaxDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateTaxAsync([FromBody] RequestTaxDto requestTaxDto)
    {
        var tax = await serviceTax.CreateTaxAsync(requestTaxDto);

        return StatusCode(StatusCodes.Status201Created, tax);
    }

    /// <summary>
    /// Updates a tax by its ID.
    /// </summary>
    /// <param name="taxId">Id to look for</param>
    /// <param name="requestTaxDto">Tax request model to be updated</param>
    /// <returns>Updated tax</returns>
    [HttpPut("{taxId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseTaxDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateTaxAsync(long taxId, [FromBody] RequestTaxDto requestTaxDto)
    {
        var tax = await serviceTax.UpdateTaxAsync(taxId, requestTaxDto);

        return StatusCode(StatusCodes.Status200OK, tax);
    }

    /// <summary>
    /// Deletes a tax by its ID.
    /// </summary>
    /// <param name="taxId">Id to look for</param>
    /// <returns>
    /// A boolean indicating whether the delete operation was successful.
    /// </returns>
    [HttpDelete("{taxId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteAsync(long taxId)
    {
        var result = await serviceTax.DeleteAsync(taxId);

        return StatusCode(StatusCodes.Status200OK, result);
    }
}