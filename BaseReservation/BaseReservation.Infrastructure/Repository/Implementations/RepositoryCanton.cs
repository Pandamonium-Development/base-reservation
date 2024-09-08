using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCanton(BaseReservationContext context) : IRepositoryCanton
{
    /// <inheritdoc />
    public async Task<Canton?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Canton))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Canton>()
                    .Include(m => m.IdProvinciaNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Canton>> ListAllByProvinciaAsync(byte idProvincia)
    {
        var collection = await context.Set<Canton>()
            .Where(m => m.IdProvincia == idProvincia)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}