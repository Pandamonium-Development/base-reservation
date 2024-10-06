using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventory
{
    /// <summary>
    /// Create a new Inventory.
    /// </summary>
    /// <param name="inventory">The Inventory entity to be added.</param>
    /// <returns>Inventory</returns>
    Task<Inventory> CreateInventoryAsync(Inventory inventory);

    /// <summary>
    /// Updates an existing Inventory
    /// </summary>
    /// <param name="inventory"> The Inventory entity to update.</param>
    /// <returns>Inventory</returns>
    Task<Inventory> UpdateInventoryAsync(Inventory inventory);

    /// <summary>
    /// Lists all active Inventory entities.
    /// </summary>
    /// <returns>ICollection of Inventory</returns>
    Task<ICollection<Inventory>> ListAllAsync();

    /// <summary>
    /// Lists all active Inventory entities for a specific branch.
    /// </summary>
    /// <param name="branchId">Branch id to look for</param>
    /// <returns>ICollection of Inventory</returns>
    Task<ICollection<Inventory>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Finds an active Inventory by its unique identifier, including related Inventory Products.
    /// </summary>
    /// <param name="id">The Inventory entity if found. </param>
    /// <returns>Inventory if founded, otherwise null</returns>
    Task<Inventory?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if an active Inventory with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsInventoryAsync(short id);

    /// <summary>
    /// Delete an Inventory by setting its "Activo" property to false
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory to delete.</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    Task<bool> DeleteInventoryAsync(short id);
}