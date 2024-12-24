
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventoryProductTransaction
{
    /// <summary>
    /// Lists all InventoryProductTransaction entities associated with a specific Inventario.
    /// </summary>
    /// <param name="inventoryId"> The unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventoryProductTransaction</returns>
    Task<ICollection<InventoryProductTransaction>> ListAllByInventoryAsync(short inventoryId);

    /// <summary>
    /// Lists all InventoryProductTransaction entities associated with a specific Producto.
    /// </summary>
    /// <param name="productId">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventoryProductTransaction</returns>
    Task<ICollection<InventoryProductTransaction>> ListAllByProductAsync(short productId);

    /// <summary>
    /// Creates a new InventoryProductTransaction and updates the related InventarioProducto based on the movement type.
    /// </summary>
    /// <param name="inventoryProductTransaction">The InventoryProductTransaction entity to be added.</param>
    /// <returns>InventoryProductTransaction</returns>
    /// <exception cref="RequestFailedException"></exception>
    Task<bool> CreateInventoryProductTransactionAsync(InventoryProductTransaction inventoryProductTransaction);
}