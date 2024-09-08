using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCategoria(BaseReservationContext context) : IRepositoryCategoria
{
    /// <inheritdoc />
    public async Task<Categoria?> FindByIdAsync(byte id)
    {
        return await context.Set<Categoria>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Categoria>> ListAllAsync()
    {
        var collection = await context.Set<Categoria>().AsNoTracking().ToListAsync();
        return collection;
    }
}