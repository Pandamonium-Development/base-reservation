using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursal
{
    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="sucursal">Branch model to be added</param>
    /// <returns>Sucursal</returns>
    Task<Sucursal> CreateSucursalAsync(Sucursal sucursal);

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="sucursal">Branch model to be updated</param>
    /// <returns>Sucursal</returns>
    Task<Sucursal> UpdateSucursalAsync(Sucursal sucursal);

    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of Sucursal</returns>
    Task<ICollection<Sucursal>> ListAllAsync();

    /// <summary>
    /// Get list of all branches by role
    /// </summary>
    /// <param name="rol">Role to look for</param>
    /// <returns>ICollection of Sucursal</returns>
    Task<ICollection<Sucursal>> ListAllByRoleAsync(string rol);

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Sucursal if founded, otherwise null</returns>
    Task<Sucursal?> FindByIdAsync(byte id);

    /// <summary>
    /// Validate if exists branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsSucursalAsync(byte id);
}