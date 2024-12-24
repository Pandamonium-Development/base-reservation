using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCategory
{
    /// <summary>
    /// Get list of all existing categories
    /// </summary>
    /// <returns>ICollection of Category</returns>
    Task<ICollection<Category>> ListAllAsync();

    /// <summary>
    /// Get exact category according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Category</returns>
    Task<Category?> FindByIdAsync(byte id);
}