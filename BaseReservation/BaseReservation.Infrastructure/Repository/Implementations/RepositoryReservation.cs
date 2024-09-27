using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservation(BaseReservationContext context) : IRepositoryReservation
{
    /// <inheritdoc />
    public async Task<Reservation> CreateReservationAsync(Reservation reserva)
    {
        var result = context.Reservations.Add(reserva);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Reservation> UpdateReservationAsync(Reservation reservation)
    {
        context.Reservations.Update(reservation);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(reservation.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Reservation?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reservation))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Reservation>()
            .Include(a => a.ReservationDetails)
            .ThenInclude(a => a.ServiceIdNavigation!)/*TO DO*/
            .ThenInclude(a => a.TypeServiceIdNavigation)
            .Include(a => a.BranchIdNavigation)
            .Include(a => a.ReservationQuestions)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsReservationAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reservation))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Reservation>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reservation>> ListAllAsync()
    {
        var collection = await context.Set<Reservation>()
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId, DateOnly date)
    {
        var collection = await context.Set<Reservation>()
            .AsNoTracking()
            .Where(m => m.BranchId == branchId && m.Date == date)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<Reservation>()
            .AsNoTracking()
            .Where(m => m.BranchId == branchId)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId, DateOnly startDate, DateOnly endDate)
    {
        var collection = await context.Set<Reservation>()
            .AsNoTracking()
            .Where(m => m.BranchId == branchId && m.Date >= startDate && m.Date <= endDate)
            .ToListAsync();
        return collection;
    }
}