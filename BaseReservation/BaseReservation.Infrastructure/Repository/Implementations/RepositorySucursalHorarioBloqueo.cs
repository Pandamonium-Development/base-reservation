using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursalHorarioBloqueo(BaseReservationContext context) : IRepositorySucursalHorarioBloqueo
{
    /// <summary>
    /// Create a branch schedule block
    /// </summary>
    /// <param name="sucursalHorarioBloqueo">Branch schedule block model to be added</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    public async Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo)
    {
        var result = context.SucursalHorarioBloqueos.Add(sucursalHorarioBloqueo);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Create multiple schedule branch blocks
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <param name="sucursalHorarioBloqueos">List of schedule branch blocks to be added</param>
    /// <returns>True if all items were saved, if not, false</returns>
    public async Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<SucursalHorarioBloqueo> sucursalHorarioBloqueos)
    {
        var result = true;
        var bloqueosExistentes = await ListAllBySucursalHorarioAsync(idSucursalHorario);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.SucursalHorarioBloqueos.RemoveRange(bloqueosExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && bloqueosExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.SucursalHorarioBloqueos.AddRange(sucursalHorarioBloqueos);
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
    /// Validate if exists branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block Id</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsSucursalHorarioBloqueoAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Get branch schedule block with specific id
    /// </summary>
    /// <param name="id">Branch schedule block id</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    public async Task<SucursalHorarioBloqueo?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all branch schedule blocks by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of SucursalHorarioBloqueo</returns>
    public async Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<SucursalHorarioBloqueo>()
         .Include(m => m.IdSucursalHorarioNavigation)
         .ThenInclude(m => m.IdHorarioNavigation)
         .Where(a => a.IdSucursalHorarioNavigation.IdSucursal == idSucursal)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all branch schedule blocks by branch schedule
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <returns>ICollection of SucursalHorarioBloqueo</returns>
    public async Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalHorarioAsync(short idSucursalHorario)
    {
        var collection = await context.Set<SucursalHorarioBloqueo>()
         .Where(a => a.IdSucursalHorario == idSucursalHorario)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="sucursalHorarioBloqueo">Branch schedule block model to be added</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    public async Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo)
    {
        context.SucursalHorarioBloqueos.Update(sucursalHorarioBloqueo);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(sucursalHorarioBloqueo.Id);
        return response!;
    }
}