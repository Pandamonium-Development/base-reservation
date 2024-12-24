using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventoryProduct
{
    /// <summary>
    /// Checks if an InventoryProduct with the specified identifier exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the InventoryProduct.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsInventoryProductAsync(long id);

    /// <summary>
    /// Finds an InventarioProdcuto by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the InventoryProduct</param>
    /// <returns>InventoryProduct if founded, otherwise null</returns>
    Task<InventoryProduct?> FindByIdAsync(long id);

    /// <summary>
    /// Lists all InventoryProduct entities associated with a specific Inventario.
    /// </summary>
    /// <param name="inventoryId">The unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventoryProduct</returns>
    Task<ICollection<InventoryProduct>> ListAllByInventoryAsync(short inventoryId);

    /// <summary>
    /// Lists all InventoryProduct entities associated with a specific Producto.
    /// </summary>
    /// <param name="productId">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventoryProduct </returns>
    Task<ICollection<InventoryProduct>> ListAllByProductAsync(short productId);

    /// <summary>
    /// Creates a new InventoryProduct
    /// </summary>
    /// <param name="inventoryProduct">The InventarioProdcuto entity to be added.</param>
    /// <returns>InventoryProduct</returns>
    Task<InventoryProduct> CreateInventoryProductAsync(InventoryProduct inventoryProduct);

    /// <summary>
    /// Creates multiple InventoryProduct entities.
    /// </summary>
    /// <param name="inventoryProduct">A collection of InventoryProduct entities to be added.</param>
    /// <returns>True if were added, if not, false</returns>
    Task<bool> CreateInventoryProductAsync(IEnumerable<InventoryProduct> inventoryProduct);

    /// <summary>
    /// Updates an existing InventoryProduct entity.
    /// </summary>
    /// <param name="inventoryProduct">The InventoryProduct entity to update.</param>
    /// <returns>InventoryProduct</returns>
    Task<InventoryProduct> UpdateInventoryProductAsync(InventoryProduct inventoryProduct);
}