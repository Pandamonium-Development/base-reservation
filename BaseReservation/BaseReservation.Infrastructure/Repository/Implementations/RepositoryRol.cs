using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryRol(BaseReservationContext context) : IRepositoryRol
{
    /// <summary>
    /// Get role with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if founded, otherwise null</returns>
    public async Task<Rol?> FindByIdAsync(byte id)
    {
        return await context.Set<Rol>().FindAsync(id);
    }

    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>ICollection of Rol</returns>
    public async Task<ICollection<Rol>> ListAllAsync()
    {
        var collection = await context.Set<Rol>().AsNoTracking().ToListAsync();
        return collection;
    }
}