using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUnidadMedida
{
    /// <summary>
    /// Get list of all of units of measure 
    /// </summary>
    /// <returns>ICollection of UnidadMedida</returns>
    Task<ICollection<UnidadMedida>> ListAllAsync();

    /// <summary>
    /// Get unit of measure with specific id
    /// </summary>
    /// <param name="id">Unit of measure id</param>
    /// <returns>Unit of measure if founded, otherwise null</returns>
    Task<UnidadMedida?> FindByIdAsync(byte id);
}