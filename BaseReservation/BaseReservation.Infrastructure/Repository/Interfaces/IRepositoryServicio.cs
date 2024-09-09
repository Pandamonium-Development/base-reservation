using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryServicio
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>ICollection of Servicio</returns>
    Task<ICollection<Servicio>> ListAllAsync();

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Servicio if founded, otherwise null</returns>
    Task<Servicio?> FindByIdAsync(byte id);

    /// <summary>
    /// Create service
    /// </summary>
    /// <param name="servicio">Service model to be added</param>
    /// <returns>Servicio</returns>
    Task<Servicio> CreateServicioAsync(Servicio servicio);

    /// <summary>
    /// Update service
    /// </summary>
    /// <param name="servicio">Service model to be updated</param>
    /// <returns>Servicio</returns>    
    Task<Servicio> UpdateServicioAsync(Servicio servicio);

    /// <summary>
    /// Validate if exists service
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsServicioAsync(byte id);
}