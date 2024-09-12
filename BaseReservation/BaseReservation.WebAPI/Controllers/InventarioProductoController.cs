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
/// Controller for managing inventory products.
/// </summary>
/// <param name="serviceInventarioProducto">The service used for inventory product operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class InventarioProductoController(IServiceInventarioProducto serviceInventarioProducto) : ControllerBase
{
    /// <summary>
    /// Retrieves a specific inventory product by its ID.
    /// </summary>
    /// <param name="idInventarioProducto">The ID of the inventory product.</param>
    /// <returns>The details of the specified inventory product.</returns>
    [HttpGet("{idInventarioProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventarioProductoDto))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllInventarioProductosByInventarioAsync(long idInventarioProducto)
    {
        var inventoryProducts = await serviceInventarioProducto.FindByIdAsync(idInventarioProducto);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }

    /// <summary>
    /// Retrieves all inventory products for a given inventory.
    /// </summary>
    /// <param name="idInventario">The ID of the inventory.</param>
    /// <returns>A list of inventory products for the specified inventory.</returns>
    [HttpGet("~/api/Inventario/{idInventario}/Productos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventarioProductoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllInventarioProductosByInventarioAsync(short idInventario)
    {
        var inventoryProducts = await serviceInventarioProducto.ListAllByInventarioAsync(idInventario);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }

    /// <summary>
    /// Retrieves all inventory products for a given product.
    /// </summary>
    /// <param name="idProducto">The ID of the product.</param>
    /// <returns>A list of inventory products for the specified product.</returns>
    [HttpGet("~/api/Producto/{idProducto}/Inventarios")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventarioProductoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllInventarioProductosByProductoAsync(short idProducto)
    {
        var inventoryProducts = await serviceInventarioProducto.ListAllByProductoAsync(idProducto);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }

    /// <summary>
    /// Creates a new inventory product.
    /// </summary>
    /// <param name="inventarioProducto">The inventory product data to be created.</param>
    /// <returns>The details of the created inventory product.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventarioProductoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateInventarioProductoAsync([FromBody] RequestInventarioProductoDto inventarioProducto)
    {
        ArgumentNullException.ThrowIfNull(inventarioProducto);
        var inventoryProducts = await serviceInventarioProducto.CreateProductoInventarioAsync(inventarioProducto);
        return StatusCode(StatusCodes.Status201Created, inventoryProducts);
    }

    /// <summary>
    /// Creates multiple inventory products in bulk.
    /// </summary>
    /// <param name="inventarioProductos">A collection of inventory product data to be created.</param>
    /// <returns>A boolean indicating whether the creation was successful.</returns>
    [HttpPost("Bulk")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateInventarioProductosAsync([FromBody] IEnumerable<RequestInventarioProductoDto> inventarioProducto)
    {
        ArgumentNullException.ThrowIfNull(inventarioProducto);
        var inventoryProducts = await serviceInventarioProducto.CreateProductoInventarioAsync(inventarioProducto);
        return StatusCode(StatusCodes.Status201Created, inventoryProducts);
    }

    /// <summary>
    /// Updates an existing inventory product.
    /// </summary>
    /// <param name="idInventarioProducto">The ID of the inventory product to update.</param>
    /// <param name="inventarioProducto">The updated inventory product data.</param>
    /// <returns>The details of the updated inventory product.</returns>
    [HttpPut("{idInventarioProducto}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventarioProductoDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateInvetarioAsync(long idInventarioProducto, [FromBody] RequestInventarioProductoDto inventarioProducto)
    {
        ArgumentNullException.ThrowIfNull(inventarioProducto);
        var inventoryProducts = await serviceInventarioProducto.UpdateProductoInventarioAsync(idInventarioProducto, inventarioProducto);
        return StatusCode(StatusCodes.Status200OK, inventoryProducts);
    }
}
