using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDistrito
{
    /// <summary>
    /// Get list of districts base on a parent State
    /// </summary>
    /// <param name="idCanton">Id state parent</param>
    /// <returns>ICollection of Distrito</returns>
    Task<ICollection<Distrito>> ListAllByCantonAsync(byte idCanton);

    /// <summary>
    /// Get exact district detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Distrito</returns>
    Task<Distrito?> FindByIdAsync(byte id);
}