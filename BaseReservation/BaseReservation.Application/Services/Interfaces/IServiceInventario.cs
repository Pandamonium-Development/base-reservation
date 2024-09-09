using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventario
{
    /// <summary>
    /// Get list of all inventories
    /// </summary>
    /// <returns>ICollection of ResponseInventarioDto</returns>
    Task<ICollection<ResponseInventarioDto>> ListAllAsync();

    /// <summary>
    /// Get list of all inventories by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of ResponseInventarioDto</returns>
    Task<ICollection<ResponseInventarioDto>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Finds Inventory by its unique identifier
    /// </summary>
    /// <param name="id">The Inventory entity id</param>
    /// <returns>ResponseInventarioDto if founded, otherwise null</returns>
    Task<ResponseInventarioDto> FindByIdAsync(short id);

    /// <summary>
    /// Create inventario
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="inventarioDto">Inventory model request to be added</param>
    /// <returns>ResponseInventarioDto</returns>
    Task<ResponseInventarioDto> CreateInventarioAsync(byte idSucursal, RequestInventarioDto inventarioDto);

    /// <summary>
    /// Update existing inventarory
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="id">Inventary id</param>
    /// <param name="inventarioDto">Inventory model request to be updated</param>
    /// <returns>ResponseInventarioDto</returns>
    Task<ResponseInventarioDto> UpdateInventarioAsync(byte idSucursal, short id, RequestInventarioDto inventarioDto);

    /// <summary>
    /// Delete inventory
    /// </summary>
    /// <param name="id">Inventory id to be deleted</param>
    /// <returns>True if was deleted successfully, if not, false</returns>
    Task<bool> DeleteInventarioAsync(short id);
}