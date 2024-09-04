using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTipoServicio(BaseReservationContext context) : IRepositoryTipoServicio
{
    /// <summary>
    /// Get a Type of service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>TipoServicio if founded, otherwise null</returns>
    public async Task<TipoServicio?> FindByIdAsync(byte id)
    {
        return await context.Set<TipoServicio>().FindAsync(id);
    }

    /// <summary>
    /// Get list of all types of service
    /// </summary>
    /// <returns>ICollection of TipoServicio</returns>
    public async Task<ICollection<TipoServicio>> ListAllAsync()
    {
        var collection = await context.Set<TipoServicio>().AsNoTracking().ToListAsync();
        return collection;
    }
}