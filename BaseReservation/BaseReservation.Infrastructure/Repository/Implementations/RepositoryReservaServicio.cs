using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservaServicio(BaseReservationContext context) : IRepositoryDetalleReserva
{
    /// <summary>
    /// Create multiple details reservation
    /// </summary>
    /// <param name="idReserva">Reservation id</param>
    /// <param name="detallesReserva">List of detail reservation</param>
    /// <returns>True if were added, if not, false</returns>
    public async Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<DetalleReserva> detallesReserva)
    {
        var result = true;
        var reservasExistentes = await ListAllByReservaAsync(idReserva);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.DetalleReservas.RemoveRange(reservasExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && reservasExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.DetalleReservas.AddRange(detallesReserva);
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

    /// <summary>
    /// Get detail reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>DetalleReserva if founded, otherwise null</returns>
    public async Task<DetalleReserva?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetalleReserva))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetalleReserva>()
                .Include(m => m.IdReservaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all details reservation
    /// </summary>
    /// <param name="idReserva">Reservation id</param>
    /// <returns>ICollection of DetalleReserva</returns>
    public async Task<ICollection<DetalleReserva>> ListAllByReservaAsync(int idReserva)
    {
        var collection = await context.Set<DetalleReserva>()
         .Include(m => m.IdServicioNavigation)
         .AsNoTracking()
         .Where(m => m.IdReserva == idReserva)
         .ToListAsync();
        return collection;
    }
}
