using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDistrito(BaseReservationContext context) : IRepositoryDistrito
{
    /// <summary>
    /// Get exact district detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Distrito</returns>
    public async Task<Distrito?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Distrito))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Distrito>()
                    .Include(m => m.IdCantonNavigation)
                    .ThenInclude(m => m.IdProvinciaNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of districts base on a parent State
    /// </summary>
    /// <param name="idCanton">Id state parent</param>
    /// <returns>ICollection of Distrito</returns>
    public async Task<ICollection<Distrito>> ListAllByCantonAsync(byte idCanton)
    {
        var collection = await context.Set<Distrito>()
            .Where(m => m.IdCanton == idCanton)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}
