using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCategoria(BaseReservationContext context) : IRepositoryCategoria
{
    /// <summary>
    /// Get exact category according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Categoria</returns>
    public async Task<Categoria?> FindByIdAsync(byte id)
    {
        return await context.Set<Categoria>().FindAsync(id);
    }

    /// <summary>
    /// Get list of all existing categories
    /// </summary>
    /// <returns>ICollection of Categoria</returns>
    public async Task<ICollection<Categoria>> ListAllAsync()
    {
        var collection = await context.Set<Categoria>().AsNoTracking().ToListAsync();
        return collection;
    }
}