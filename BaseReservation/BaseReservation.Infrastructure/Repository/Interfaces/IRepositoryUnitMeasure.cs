using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUnitMeasure
{
    /// <summary>
    /// Get list of all of units of measure 
    /// </summary>
    /// <returns>ICollection of UnitMeasure</returns>
    Task<ICollection<UnitMeasure>> ListAllAsync();

    /// <summary>
    /// Get unit of measure with specific id
    /// </summary>
    /// <param name="id">Unit of measure id</param>
    /// <returns>Unit of measure if founded, otherwise null</returns>
    Task<UnitMeasure?> FindByIdAsync(byte id);
}