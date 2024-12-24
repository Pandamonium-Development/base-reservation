using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTypeService
{
    /// <summary>
    /// Get list of all types of service
    /// </summary>
    /// <returns>ICollection of TypeService</returns>
    Task<ICollection<TypeService>> ListAllAsync();

    /// <summary>
    /// Get a Type of service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>TypeService if founded, otherwise null</returns>
    Task<TypeService?> FindByIdAsync(byte id);
}