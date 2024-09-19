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
/// Controller for managing orders (Pedidos).
/// </summary>
/// <param name="servicePedido">The service used for order operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class PedidoController(IServicePedido servicePedido) : ControllerBase
{
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>A list of orders.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponsePedidoDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> ListAllAsync()
    {
        var orders = await servicePedido.ListAllAsync();
        return StatusCode(StatusCodes.Status200OK, orders);
    }

    /// <summary>
    /// Retrieves an order by its ID.
    /// </summary>
    /// <param name="idPedido">The ID of the order to retrieve.</param>
    /// <returns>The details of the specified order.</returns>
    [HttpGet("{idPedido}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponsePedidoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> FindByIdAsync(long idPedido)
    {
        var order = await servicePedido.FindByIdAsync(idPedido);
        return StatusCode(StatusCodes.Status200OK, order);
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    /// <param name="pedido">The order data to be created.</param>
    /// <returns>The details of the created order.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponsePedidoDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreatePedidoAsync([FromBody] RequestPedidoDto pedido)
    {
        ArgumentNullException.ThrowIfNull(pedido);
        var result = await servicePedido.CreatePedidoAsync(pedido);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}