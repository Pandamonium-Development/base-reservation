using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCategoria
{
    /// <summary>
    /// Get list of all existing categories
    /// </summary>
    /// <returns>ICollection of Categoria</returns>
    Task<ICollection<Categoria>> ListAllAsync();

    /// <summary>
    /// Get exact category according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Categoria</returns>
    Task<Categoria?> FindByIdAsync(byte id);
}