using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursalFeriado(BaseReservationContext context) : IRepositorySucursalFeriado
{
    /// <summary>
    /// Create branch holidays
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursalFeriados">List of branch holidays to be added</param>
    /// <returns>True if all were added, if not, false</returns>
    public async Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<SucursalFeriado> sucursalFeriados)
    {
        var result = true;
        var feriadosExistentes = await ListAllBySucursalAsync(idSucursal);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.SucursalFeriados.RemoveRange(feriadosExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && feriadosExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.SucursalFeriados.AddRange(sucursalFeriados);
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
                throw new RequestFailedException("Error al guardar feriados", exc);
            }

        });

        return result;
    }

    /// <summary>
    /// Get list of all branch holidays by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    public async Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<SucursalFeriado>()
                .AsNoTracking()
                .Include(m => m.IdFeriadoNavigation)
                .Where(m => m.IdSucursal == idSucursal)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all branch holidays by branch and year
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="anno">Year to look for</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    public async Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, short anno)
    {
        var collection = await context.Set<SucursalFeriado>()
                .AsNoTracking()
                .Include(m => m.IdFeriadoNavigation)
                .Where(m => m.IdSucursal == idSucursal && m.Anno == anno)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all branch holidays by branch, start date and end date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="fechaInicio">Start date to filter</param>
    /// <param name="fechaFin">End date to filter</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    public async Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin)
    {
        var collection = await context.Set<SucursalFeriado>()
                .AsNoTracking()
                .Include(m => m.IdFeriadoNavigation)
                .Where(m => m.IdSucursal == idSucursal && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>SucursalFeriado if founded, otherwise null</returns>
    public async Task<SucursalFeriado?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalFeriado))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<SucursalFeriado>()
                .Include(m => m.IdFeriadoNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }
}