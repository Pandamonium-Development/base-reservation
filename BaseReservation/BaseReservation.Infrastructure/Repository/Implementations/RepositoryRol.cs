using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryRol(BaseReservationContext context) : IRepositoryRol
{
    /// <inheritdoc />
    public async Task<Rol?> FindByIdAsync(byte id)
    {
        return await context.Set<Rol>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Rol>> ListAllAsync()
    {
        var collection = await context.Set<Rol>().AsNoTracking().ToListAsync();
        return collection;
    }
}