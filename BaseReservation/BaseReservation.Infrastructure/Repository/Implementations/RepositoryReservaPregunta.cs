using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservaPregunta(BaseReservationContext context) : IRepositoryReservaPregunta
{
    /// <inheritdoc />
    public async Task<ReservaPregunta?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<ReservaPregunta>()
            .Include(a => a.IdReservaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<ReservaPregunta>> ListAllAsync()
    {
        var collection = await context.Set<ReservaPregunta>()
            .Include(a => a.IdReservaNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}