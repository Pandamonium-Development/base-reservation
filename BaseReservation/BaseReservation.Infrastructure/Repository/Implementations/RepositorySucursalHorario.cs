using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursalHorario(BaseReservationContext context) : IRepositorySucursalHorario
{
    public async Task<bool> CreateSucursalHorariosAsync(byte idSucursal, IEnumerable<SucursalHorario> sucursalHorarios)
    {
        var result = true;
        var sucursalHorariosExistentes = await GetHorariosBySucursalAsync(idSucursal);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                RemoverBloqueos(sucursalHorariosExistentes.Select(m => m.SucursalHorarioBloqueos));
                context.SucursalHorarios.RemoveRange(sucursalHorariosExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && sucursalHorariosExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    ReordenarBloqueos(sucursalHorariosExistentes, sucursalHorarios);
                    context.SucursalHorarios.AddRange(sucursalHorarios);
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
                throw new RequestFailedException("Error al guardar horarios", exc);
            }
        });

        return result;
    }

    public async Task<ICollection<SucursalHorario>> GetHorariosBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<SucursalHorario>()
           .AsNoTracking()
           .Include(m => m.IdHorarioNavigation)
           .Include(v => v.SucursalHorarioBloqueos)
           .Where(m => m.IdSucursal == idSucursal)
           .ToListAsync();
        return collection;
    }

    public async Task<SucursalHorario?> GetHorarioBySucursalByDiaAsync(byte idSucursal, DiaSemana dia)
    {
        var horarioSucursal = await context.Set<SucursalHorario>()
          .AsNoTracking()
          .Include(m => m.IdHorarioNavigation)
          .Include(v => v.SucursalHorarioBloqueos)
          .FirstOrDefaultAsync(m => m.IdSucursal == idSucursal && m.IdHorarioNavigation.Dia == dia);
        return horarioSucursal;
    }

    public async Task<SucursalHorario?> GetSucursalHorarioByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(SucursalHorario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<SucursalHorario>()
                .Include(m => m.IdHorarioNavigation)
                .Include(m => m.IdSucursalNavigation)
                .Include(m => m.SucursalHorarioBloqueos)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    private void RemoverBloqueos(IEnumerable<ICollection<SucursalHorarioBloqueo>> bloqueosExistentes)
    {
        foreach (var horarioExistente in bloqueosExistentes)
        {
            context.SucursalHorarioBloqueos.RemoveRange(horarioExistente);
        }
    }

    private void ReordenarBloqueos(ICollection<SucursalHorario> sucursalHorariosExistentes, IEnumerable<SucursalHorario> sucursalHorarios)
    {
        foreach (var item in sucursalHorarios)
        {
            var existente = sucursalHorariosExistentes.SingleOrDefault(m => m.IdSucursal == item.IdSucursal && m.IdHorario == item.IdHorario);
            if (existente != null && existente.SucursalHorarioBloqueos.Any())
            {
                var listaBloqueos = existente.SucursalHorarioBloqueos.ToList();
                listaBloqueos.ForEach(m => m.Id = 0);
                item.SucursalHorarioBloqueos = listaBloqueos;
            }
        }
    }
}
