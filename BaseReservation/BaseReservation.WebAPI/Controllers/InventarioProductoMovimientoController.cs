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
/// Controller for managing inventory product movements.
/// </summary>
/// <param name="serviceInventarioProductoMovimiento">The service used for inventory product movement operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class InventarioProductoMovimientoController(IServiceInventarioProductoMovimiento serviceInventarioProductoMovimiento) : ControllerBase
{
    /// <summary>
    /// Creates a new inventory product movement record.
    /// </summary>
    /// <param name="inventarioProductoMovimientoDto">The inventory product movement data to be created.</param>
    /// <returns>The details of the created inventory product movement.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventarioProductoMovimientoDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateInventarioMovimientoProductoAsync([FromBody] RequestInventarioProductoMovimientoDto inventarioProductoMovimientoDto)
    {
        ArgumentNullException.ThrowIfNull(inventarioProductoMovimientoDto);
        var inventory = await serviceInventarioProductoMovimiento.CreateInventarioMovimientoProductoAsync(inventarioProductoMovimientoDto);
        return StatusCode(StatusCodes.Status201Created, inventory);
    }

    /// <summary>
    /// Retrieves all inventory product movements for a given inventory.
    /// </summary>
    /// <param name="idInventario">The ID of the inventory.</param>
    /// <returns>A list of inventory product movements for the specified inventory.</returns>
    [HttpGet("~/api/Inventario/{idInventario}/Movimientos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventarioProductoMovimientoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByInventarioAsync(short idInventario)
    {
        var inventories = await serviceInventarioProductoMovimiento.ListAllByInventarioAsync(idInventario);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }

    /// <summary>
    /// Retrieves all inventory product movements for a given product.
    /// </summary>
    /// <param name="idProducto">The ID of the product.</param>
    /// <returns>A list of inventory product movements for the specified product.</returns>
    [HttpGet("~/api/Producto/{idProducto}/Movimientos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventarioProductoMovimientoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllByProductoAsync(short idProducto)
    {
        var inventories = await serviceInventarioProductoMovimiento.ListAllByProductoAsync(idProducto);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }
}