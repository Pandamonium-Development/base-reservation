using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventarioProductoMovimiento
{
    /// <summary>
    /// Get list of all inventory product movements by inventory
    /// </summary>
    /// <param name="idInventario">Inventory id</param>
    /// <returns>ICollection of ResponseInventarioProductoMovimientoDto</returns>
    Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByInventarioAsync(short idInventario);

    /// <summary>
    /// Get list of all inventory product movements by product
    /// </summary>
    /// <param name="idProducto">Product id</param>
    /// <returns>ICollection of ResponseInventarioProductoMovimientoDto</returns>
    Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByProductoAsync(short idProducto);

    /// <summary>
    /// Create inventory product movement
    /// </summary>
    /// <param name="inventarioProductoMovimientoDto">Inventory product movement to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateInventarioMovimientoProductoAsync(RequestInventarioProductoMovimientoDto inventarioProductoMovimientoDto);
}