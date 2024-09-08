using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursalHorarioBloqueo(BaseReservationContext context) : IRepositorySucursalHorarioBloqueo
{
    /// <inheritdoc />
    public async Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo)
    {
        var result = context.SucursalHorarioBloqueos.Add(sucursalHorarioBloqueo);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<bool> ExistsSucursalHorarioBloqueoAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<SucursalHorarioBloqueo?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalHorarioAsync(short idSucursalHorario)
    {
        var collection = await context.Set<SucursalHorarioBloqueo>()
         .Where(a => a.IdSucursalHorario == idSucursalHorario)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo)
    {
        context.SucursalHorarioBloqueos.Update(sucursalHorarioBloqueo);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(sucursalHorarioBloqueo.Id);
        return response!;
    }
}