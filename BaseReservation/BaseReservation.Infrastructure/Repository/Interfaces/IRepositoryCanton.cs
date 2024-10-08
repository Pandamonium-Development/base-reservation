using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCanton
{
    /// <summary>
    /// Get list of states base on a parent province
    /// </summary>
    /// <param name="provinceId">Id province parent</param>
    /// <returns>ICollection of Canton </returns> 
    Task<ICollection<Canton>> ListAllByProvinceAsync(byte provinceId);

    /// <summary>
    /// Get exact state according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Canton</returns>
    Task<Canton?> FindByIdAsync(byte id);
}