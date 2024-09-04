using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProvincia(BaseReservationContext context) : IRepositoryProvincia
{
    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Provincia if founded, otherwise null</returns>
    public async Task<Provincia?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Provincia))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Provincia>().AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>ICollection of Provincia</returns>
    public async Task<ICollection<Provincia>> ListAllAsync()
    {
        var collection = await context.Set<Provincia>().AsNoTracking().ToListAsync();
        return collection;
    }
}