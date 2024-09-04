using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCanton(BaseReservationContext context) : IRepositoryCanton
{
    /// <summary>
    /// Get exact state according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Canton</returns>
    public async Task<Canton?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Canton))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Canton>()
                    .Include(m => m.IdProvinciaNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of states base on a parent province
    /// </summary>
    /// <param name="idProvincia">Id province parent</param>
    /// <returns>ICollection of Canton </returns> 
    public async Task<ICollection<Canton>> ListAllByProvinciaAsync(byte idProvincia)
    {
        var collection = await context.Set<Canton>()
            .Where(m => m.IdProvincia == idProvincia)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}