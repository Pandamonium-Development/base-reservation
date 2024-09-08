using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventario
{
    /// <summary>
    /// Create a new Inventory.
    /// </summary>
    /// <param name="inventario">The Inventory entity to be added.</param>
    /// <returns>Inventario</returns>
    Task<Inventario> CreateInventarioAsync(Inventario inventario);

    /// <summary>
    /// Updates an existing Inventory
    /// </summary>
    /// <param name="inventario"> The Inventory entity to update.</param>
    /// <returns>Inventario</returns>
    Task<Inventario> UpdateInventarioAsync(Inventario inventario);

    /// <summary>
    /// Lists all active Inventory entities.
    /// </summary>
    /// <returns>ICollection of Inventario</returns>
    Task<ICollection<Inventario>> ListAllAsync();

    /// <summary>
    /// Lists all active Inventory entities for a specific branch.
    /// </summary>
    /// <param name="idSucursal"></param>
    /// <returns>ICollection of Inventario</returns>
    Task<ICollection<Inventario>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Finds an active Inventory by its unique identifier, including related Inventory Products.
    /// </summary>
    /// <param name="id">The Inventory entity if found. </param>
    /// <returns>Inventario if founded, otherwise null</returns>
    Task<Inventario?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if an active Inventory with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsInventarioAsync(short id);

    /// <summary>
    /// Delete an Inventory by setting its "Activo" property to false
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory to delete.</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    Task<bool> DeleteInventarioAsync(short id);
}