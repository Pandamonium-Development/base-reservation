using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProvincia
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>ICollection of Provincia</returns>
    Task<ICollection<Provincia>> ListAllAsync();

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Provincia if founded, otherwise null</returns>
    Task<Provincia?> FindByIdAsync(byte id);
}