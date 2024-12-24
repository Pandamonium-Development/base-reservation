using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryRole
{
    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>ICollection of Role</returns>
    Task<ICollection<Role>> ListAllAsync();

    /// <summary>
    /// Get role with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if founded, otherwise null</returns>    
    Task<Role?> FindByIdAsync(byte id);
}