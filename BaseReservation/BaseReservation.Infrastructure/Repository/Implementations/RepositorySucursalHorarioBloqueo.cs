using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursalHorarioBloqueo(BaseReservationContext context) : IRepositorySucursalHorarioBloqueo
{
    public async Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueolAsync(SucursalHorarioBloqueo bloqueo)
    {
        var result = context.SucursalHorarioBloqueos.Add(bloqueo);
        //aplica en la BD
        await context.SaveChangesAsync();
        //Refleja la entidad
        return result.Entity;
    }

    public async Task<bool> CreateSucursalHorarioBloqueolAsync(short idSucursalHorario, IEnumerable<SucursalHorarioBloqueo> bloqueo)
    {
        var result = true;
        var bloqueosExistentes = await GetSucursalHorarioBloqueosBySucursalHorarioAsync(idSucursalHorario);

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
                    context.SucursalHorarioBloqueos.AddRange(bloqueo);
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

    public async Task<bool> ExisteHorarioBloqueo(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    public async Task<SucursalHorarioBloqueo?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorarioBloqueo))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<SucursalHorarioBloqueo>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    public async Task<ICollection<SucursalHorarioBloqueo>> GetSucursalHorarioBloqueosBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<SucursalHorarioBloqueo>()
         .Include(m => m.IdSucursalHorarioNavigation)
         .ThenInclude(m => m.IdHorarioNavigation)
         .Where(a => a.IdSucursalHorarioNavigation.IdSucursal == idSucursal)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    public async Task<ICollection<SucursalHorarioBloqueo>> GetSucursalHorarioBloqueosBySucursalHorarioAsync(short idSucursalHorario)
    {
        var collection = await context.Set<SucursalHorarioBloqueo>()
         .Where(a => a.IdSucursalHorario == idSucursalHorario)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    public async Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueolAsync(SucursalHorarioBloqueo bloqueo)
    {
        context.SucursalHorarioBloqueos.Update(bloqueo);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(bloqueo.Id);
        return response!;
    }
}