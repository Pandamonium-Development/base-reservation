
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventarioProductoMovimiento
{
    /// <summary>
    /// Lists all InventarioProductoMovimiento entities associated with a specific Inventario.
    /// </summary>
    /// <param name="idInventario"> The unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventarioProductoMovimiento</returns>
    Task<ICollection<InventarioProductoMovimiento>> ListAllByInventarioAsync(short idInventario);

    /// <summary>
    /// Lists all InventarioProductoMovimiento entities associated with a specific Producto.
    /// </summary>
    /// <param name="idProducto">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventarioProductoMovimiento</returns>
    Task<ICollection<InventarioProductoMovimiento>> ListAllByProductoAsync(short idProducto);

    /// <summary>
    /// Creates a new InventarioProductoMovimiento and updates the related InventarioProducto based on the movement type.
    /// </summary>
    /// <param name="inventarioProductoMovimiento">The InventarioProductoMovimiento entity to be added.</param>
    /// <returns>InventarioProductoMovimiento</returns>
    /// <exception cref="RequestFailedException"></exception>
    Task<bool> CreateInventarioMovimientoProductoAsync(InventarioProductoMovimiento inventarioProductoMovimiento);
}