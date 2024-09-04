using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryRol(BaseReservationContext context) : IRepositoryRol
{
    public async Task<Rol?> FindByIdAsync(byte id)
    {
        return await context.Set<Rol>().FindAsync(id);
    }

    public async Task<ICollection<Rol>> ListAsync()
    {
        var collection = await context.Set<Rol>().AsNoTracking().ToListAsync();
        return collection;
    }
}