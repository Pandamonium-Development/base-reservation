using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventory
{
    /// <summary>
    /// Get list of all inventories
    /// </summary>
    /// <returns>ICollection of ResponseInventoryDto</returns>
    Task<ICollection<ResponseInventoryDto>> ListAllAsync();

    /// <summary>
    /// Get list of all inventories by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of ResponseInventoryDto</returns>
    Task<ICollection<ResponseInventoryDto>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Finds Inventory by its unique identifier
    /// </summary>
    /// <param name="id">The Inventory entity id</param>
    /// <returns>ResponseInventoryDto if founded, otherwise null</returns>
    Task<ResponseInventoryDto> FindByIdAsync(short id);

    /// <summary>
    /// Create inventario
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="inventoryDto">Inventory model request to be added</param>
    /// <returns>ResponseInventoryDto</returns>
    Task<ResponseInventoryDto> CreateInventoryAsync(byte branchId, RequestInventoryDto inventoryDto);

    /// <summary>
    /// Update existing inventarory
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="id">Inventary id</param>
    /// <param name="inventoryDto">Inventory model request to be updated</param>
    /// <returns>ResponseInventoryDto</returns>
    Task<ResponseInventoryDto> UpdateInventoryAsync(byte branchId, short id, RequestInventoryDto inventoryDto);

    /// <summary>
    /// Delete inventory
    /// </summary>
    /// <param name="id">Inventory id to be deleted</param>
    /// <returns>True if was deleted successfully, if not, false</returns>
    Task<bool> DeleteInventoryAsync(short id);
}