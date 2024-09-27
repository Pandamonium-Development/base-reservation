using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservationQuestion(BaseReservationContext context) : IRepositoryReservationQuestion
{
    /// <inheritdoc />
    public async Task<ReservationQuestion?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(ReservationQuestion))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<ReservationQuestion>()
            .Include(a => a.ReservationIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<ReservationQuestion>> ListAllAsync()
    {
        var collection = await context.Set<ReservationQuestion>()
            .Include(a => a.ReservationIdNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}