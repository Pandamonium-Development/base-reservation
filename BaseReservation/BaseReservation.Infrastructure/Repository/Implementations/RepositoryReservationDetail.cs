using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservationDetail(BaseReservationContext context) : IRepositoryReservationDetail
{
    /// <inheritdoc />
    public async Task<bool> CreateReservationDetailAsync(int reservationId, IEnumerable<ReservationDetail> reservationDetails)
    {
        var result = true;
        var existingReservations = await ListAllByReservationAsync(reservationId);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.ReservationDetails.RemoveRange(existingReservations);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && existingReservations.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.ReservationDetails.AddRange(reservationDetails);
                    rowsAffected = await context.SaveChangesAsync();

                    if (rowsAffected == 0)
                    {
                        await transaccion.RollbackAsync();
                        result = false;
                    }
                    else
                    {
                        await transaccion.CommitAsync();
                    }
                }
            }
            catch (Exception exc)
            {
                await transaccion.RollbackAsync();
                throw new RequestFailedException("Error al guardar servicios", exc);
            }
        });

        return result;

    }

    /// <inheritdoc />
    public async Task<ReservationDetail?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(ReservationDetail))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<ReservationDetail>()
                .Include(m => m.ReservationIdNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<ReservationDetail>> ListAllByReservationAsync(int reservationId)
    {
        var collection = await context.Set<ReservationDetail>()
         .Include(m => m.ServiceIdNavigation)
         .AsNoTracking()
         .Where(m => m.ReservationId == reservationId)
         .ToListAsync();
        return collection;
    }
}