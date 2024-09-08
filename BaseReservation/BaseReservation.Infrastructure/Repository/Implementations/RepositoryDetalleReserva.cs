using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservaServicio(BaseReservationContext context) : IRepositoryDetalleReserva
{
    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<DetalleReserva?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetalleReserva))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetalleReserva>()
                .Include(m => m.IdReservaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
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