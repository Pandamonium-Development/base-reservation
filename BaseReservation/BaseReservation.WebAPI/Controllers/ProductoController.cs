using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller for managing products (Productos).
/// </summary>
/// <param name="serviceProducto">The service used for product operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class ProductoController(IServiceProducto serviceProducto) : ControllerBase
{
    /// <summary>
    /// Retrieves a list of products, optionally excluding those associated with a specified inventory.
    /// </summary>
    /// <param name="excludeProductosInventario">Whether to exclude products associated with the inventory.</param>
    /// <param name="idInventario">The ID of the inventory to filter products by.</param>
    /// <returns>A list of products.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseProductoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync([FromQuery] bool excludeProductosInventario = false, [FromQuery] short idInventario = 0)
    {
        var products = await serviceProducto.ListAllAsync(excludeProductosInventario, idInventario);
        return StatusCode(StatusCodes.Status200OK, products);
    }

    /// <summary>
    /// Retrieves a product by its ID.
    /// </summary>
    /// <param name="idProducto">The ID of the product to retrieve.</param>
    /// <returns>The details of the specified product.</returns>
    [HttpGet("{idProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProductoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(short idProducto)
    {
        var product = await serviceProducto.FindByIdAsync(idProducto);
        return StatusCode(StatusCodes.Status200OK, product);
    }

    /// <summary>
    /// Creates a new product.
    /// </summary>
    /// <param name="producto">The product data to be created.</param>
    /// <returns>The details of the created product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseProductoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateProductoAsync([FromBody] RequestProductoDto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);
        var product = await serviceProducto.CreateProductoAsync(producto);
        return StatusCode(StatusCodes.Status201Created, product);
    }

    /// <summary>
    /// Updates an existing product by its ID.
    /// </summary>
    /// <param name="idProducto">The ID of the product to update.</param>
    /// <param name="producto">The updated product data.</param>
    /// <returns>The updated details of the product.</returns>
    [HttpPut("{idProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseProductoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateProductoAsync(short idProducto, [FromBody] RequestProductoDto producto)
    {
        ArgumentNullException.ThrowIfNull(producto);
        var product = await serviceProducto.UpdateProductoAsync(idProducto, producto);
        return StatusCode(StatusCodes.Status200OK, product);
    }
}