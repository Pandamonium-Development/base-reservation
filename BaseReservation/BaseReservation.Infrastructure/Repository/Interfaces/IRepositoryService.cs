using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryService
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>ICollection of Service</returns>
    Task<ICollection<Service>> ListAllAsync();

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Service if founded, otherwise null</returns>
    Task<Service?> FindByIdAsync(byte id);

    /// <summary>
    /// Create service
    /// </summary>
    /// <param name="service">Service model to be added</param>
    /// <returns>Service</returns>
    Task<Service> CreateServiceAsync(Service service);

    /// <summary>
    /// Update service
    /// </summary>
    /// <param name="service">Service model to be updated</param>
    /// <returns>Service</returns>    
    Task<Service> UpdateServiceAsync(Service service);

    /// <summary>
    /// Validate if exists service
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsServiceAsync(byte id);

    /// <summary>
    /// Deletes a service based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the service to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteServiceAsync(byte id);
}