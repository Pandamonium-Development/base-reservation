using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryRol
{
    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>ICollection of Rol</returns>
    Task<ICollection<Rol>> ListAllAsync();

    /// <summary>
    /// Get role with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if founded, otherwise null</returns>    
    Task<Rol?> FindByIdAsync(byte id);
}