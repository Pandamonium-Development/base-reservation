using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservaPregunta(BaseReservationContext context) : IRepositoryReservaPregunta
{
    /// <summary>
    /// Get reservation's question with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ReservaPregunta if founded, it not, false</returns>
    public async Task<ReservaPregunta?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<ReservaPregunta>()
            .Include(a => a.IdReservaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all questions reservation
    /// </summary>
    /// <returns>ICollection of ReservaPregunta</returns>
    public async Task<ICollection<ReservaPregunta>> ListAllAsync()
    {
        var collection = await context.Set<ReservaPregunta>()
            .Include(a => a.IdReservaNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}