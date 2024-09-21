using Asp.Versioning;
using BaseReservation.Application.Configuration.Pagination;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Utils;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller for managing suppliers (Proveedores).
/// </summary>
/// <param name="serviceProveedor">The service used for supplier operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ProveedorController(IServiceProveedor serviceProveedor) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of suppliers with optional pagination.
    /// </summary>
    /// <param name="paginationParameters">Optional pagination parameters.</param>
    /// <returns>A list of suppliers, potentially paginated.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseProveedorDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync([FromQuery] PaginationParameters? paginationParameters = null)
    {
        if (!paginationParameters!.Paginated) return StatusCode(StatusCodes.Status200OK, await serviceProveedor.ListAllAsync());

        var paginated = await serviceProveedor.ListAllAsync(paginationParameters);

        var metadata = new
        {
            paginated.TotalCount,
            paginated.PageSize,
            paginated.CurrentPage,
            paginated.TotalPages,
            paginated.HasNext,
            paginated.HasPrevious
        };

        Response.Headers.Add("X-Pagination", Serialization.Serialize(metadata));

        return StatusCode(StatusCodes.Status200OK, paginated);
    }

    /// <summary>
    /// Retrieves a supplier by its ID.
    /// </summary>
    /// <param name="idProveedor">The ID of the supplier to retrieve.</param>
    /// <returns>The details of the specified supplier.</returns>
    [HttpGet("{idProveedor}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProveedorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(byte idProveedor)
    {
        var supplier = await serviceProveedor.FindByIdAsync(idProveedor);
        return StatusCode(StatusCodes.Status200OK, supplier);
    }

    /// <summary>
    /// Creates a new supplier.
    /// </summary>
    /// <param name="proveedor">The supplier data to be created.</param>
    /// <returns>The details of the created supplier.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseProveedorDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateProveedorAsync([FromBody] RequestProveedorDto proveedor)
    {
        ArgumentNullException.ThrowIfNull(proveedor);
        var supplier = await serviceProveedor.CreateProveedorAsync(proveedor);
        return StatusCode(StatusCodes.Status201Created, supplier);
    }

    /// <summary>
    /// Updates an existing supplier by its ID.
    /// </summary>
    /// <param name="idProveedor">The ID of the supplier to update.</param>
    /// <param name="proveedor">The updated supplier data.</param>
    /// <returns>The updated details of the supplier.</returns>
    [HttpPut("{idProveedor}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProveedorDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateProveedorsync(byte idProveedor, [FromBody] RequestProveedorDto proveedor)
    {
        ArgumentNullException.ThrowIfNull(proveedor);
        var supplier = await serviceProveedor.UpdateProveedorsync(idProveedor, proveedor);
        return StatusCode(StatusCodes.Status200OK, supplier);
    }

    /// <summary>
    /// Deletes a supplier by its ID.
    /// </summary>
    /// <param name="idProveedor">The ID of the supplier to delete.</param>
    /// <returns>A boolean indicating the success of the deletion.</returns>
    [HttpDelete("{idProveedor}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteProveedorsyncAsync(byte idProveedor)
    {
        var supplier = await serviceProveedor.DeleteProveedorsyncAsync(idProveedor);
        return StatusCode(StatusCodes.Status200OK, supplier);
    }
}