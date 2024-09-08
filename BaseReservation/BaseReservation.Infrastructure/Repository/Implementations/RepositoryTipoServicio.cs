using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTipoServicio(BaseReservationContext context) : IRepositoryTipoServicio
{
    /// <inheritdoc />
    public async Task<TipoServicio?> FindByIdAsync(byte id)
    {
        return await context.Set<TipoServicio>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<TipoServicio>> ListAllAsync()
    {
        var collection = await context.Set<TipoServicio>().AsNoTracking().ToListAsync();
        return collection;
    }
}