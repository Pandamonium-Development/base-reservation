using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDistrict
{
    /// <summary>
    /// Get list of districts base on a parent State
    /// </summary>
    /// <param name="cantonId">Id state parent</param>
    /// <returns>ICollection of District</returns>
    Task<ICollection<District>> ListAllByCantonAsync(byte cantonId);

    /// <summary>
    /// Get exact district detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>District</returns>
    Task<District?> FindByIdAsync(byte id);
}