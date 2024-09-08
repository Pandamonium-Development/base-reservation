using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTipoServicio
{
    /// <summary>
    /// Get list of all types of service
    /// </summary>
    /// <returns>ICollection of TipoServicio</returns>
    Task<ICollection<TipoServicio>> ListAllAsync();

    /// <summary>
    /// Get a Type of service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>TipoServicio if founded, otherwise null</returns>
    Task<TipoServicio?> FindByIdAsync(byte id);
}