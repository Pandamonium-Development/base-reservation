using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReservaServicio(BaseReservationContext context) : IRepositoryReservaServicio
{
    public async Task<bool> CreateReservaServicioAsync(int idReserva, IEnumerable<ReservaServicio> reservaServicios)
    {
        var result = true;
        var reservasExistentes = await GetServiciosByReservaAsync(idReserva);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.ReservaServicios.RemoveRange(reservasExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && reservasExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.ReservaServicios.AddRange(reservaServicios);
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

    public async Task<ReservaServicio?> GetReservaServicioByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(ReservaServicio))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<ReservaServicio>()
                .Include(m => m.IdReservaNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    public async Task<ICollection<ReservaServicio>> GetServiciosByReservaAsync(int idReserva)
    {
        var collection = await context.Set<ReservaServicio>()
         .Include(m => m.IdServicioNavigation)
         .AsNoTracking()
         .Where(m => m.IdReserva == idReserva)
         .ToListAsync();
        return collection;
    }
}
