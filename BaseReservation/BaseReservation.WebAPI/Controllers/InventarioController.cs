using Asp.Versioning;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.WebAPI.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseReservation.WebAPI.Controllers;

/// <summary>
/// Controller for managing inventory operations.
/// </summary>
/// <param name="serviceInventario">The service used for inventory operations.</param>
[ApiController]
[AllowAnonymous]
[BaseReservationAuthorize]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[Authorize(Policy = "BaseReservation")]
public class InventarioController(IServiceInventario serviceInventario) : ControllerBase
{
    /// <summary>
    /// Retrieves all inventories for a given branch.
    /// </summary>
    /// <param name="idSucursal">The ID of the branch.</param>
    /// <returns>A list of inventories for the specified branch.</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseInventarioDto>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetAllInventariosAsync(byte idSucursal)
    {
        var inventories = await serviceInventario.ListAllBySucursalAsync(idSucursal);
        return StatusCode(StatusCodes.Status200OK, inventories);
    }

    /// <summary>
    /// Retrieves a specific inventory by its ID.
    /// </summary>
    /// <param name="idInventario">The ID of the inventory.</param>
    /// <returns>The details of the specified inventory.</returns>
    [HttpGet("~/api/[controller]/{idInventario}")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventarioDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> GetInventarioByIdAsync(short idInventario)
    {
        var inventory = await serviceInventario.FindByIdAsync(idInventario);
        return StatusCode(StatusCodes.Status200OK, inventory);
    }

    /// <summary>
    /// Creates a new inventory for a given branch.
    /// </summary>
    /// <param name="idSucursal">The ID of the branch.</param>
    /// <param name="inventario">The inventory data to be created.</param>
    /// <returns>The details of the created inventory.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseInventarioDto))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> CreateInventarioAsync(byte idSucursal, [FromBody] RequestInventarioDto inventario)
    {
        ArgumentNullException.ThrowIfNull(inventario);
        var inventory = await serviceInventario.CreateInventarioAsync(idSucursal, inventario);
        return StatusCode(StatusCodes.Status201Created, inventory);
    }

    /// <summary>
    /// Updates an existing inventory for a given branch.
    /// </summary>
    /// <param name="idSucursal">The ID of the branch.</param>
    /// <param name="idInventario">The ID of the inventory to update.</param>
    /// <param name="inventario">The updated inventory data.</param>
    /// <returns>The details of the updated inventory.</returns>
    [HttpPut("{idInventario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseInventarioDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> UpdateInvetarioAsync(byte idSucursal, short idInventario, [FromBody] RequestInventarioDto inventario)
    {
        ArgumentNullException.ThrowIfNull(inventario);
        var inventory = await serviceInventario.UpdateInventarioAsync(idSucursal, idInventario, inventario);
        return StatusCode(StatusCodes.Status200OK, inventory);
    }

    /// <summary>
    /// Deletes a specific inventory by its ID.
    /// </summary>
    /// <param name="idInventario">The ID of the inventory to delete.</param>
    /// <returns>A boolean indicating whether the deletion was successful.</returns>
    [HttpDelete("~/api/[controller]/{idInventario}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ErrorDetailsBaseReservation))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetailsBaseReservation))]
    public async Task<IActionResult> DeleteFeriado(short idInventario)
    {
        var inventory = await serviceInventario.DeleteInventarioAsync(idInventario);
        return StatusCode(StatusCodes.Status200OK, inventory);
    }
}