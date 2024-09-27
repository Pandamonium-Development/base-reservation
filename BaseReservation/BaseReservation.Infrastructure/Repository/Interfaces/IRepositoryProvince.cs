using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProvince
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>ICollection of Province</returns>
    Task<ICollection<Province>> ListAllAsync();

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Province if founded, otherwise null</returns>
    Task<Province?> FindByIdAsync(byte id);
}