using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryBranchSchedule(BaseReservationContext context) : IRepositoryBranchSchedule
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchSchedulesAsync(byte branchId, IEnumerable<BranchSchedule> branchSchedules)
    {
        var result = true;
        var existingBranchSchedules = await ListAllByBranchAsync(branchId);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                RemoveBlocks(existingBranchSchedules.Select(m => m.BranchScheduleBlocks));
                context.BranchSchedules.RemoveRange(existingBranchSchedules);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && existingBranchSchedules.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    ReorganizeBlocks(existingBranchSchedules, branchSchedules);
                    context.BranchSchedules.AddRange(branchSchedules);
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

    /// <inheritdoc />
    public async Task<ICollection<BranchSchedule>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<BranchSchedule>()
           .AsNoTracking()
           .Include(m => m.ScheduleIdNavigation)
           .Include(v => v.BranchScheduleBlocks)
           .Where(m => m.BranchId == branchId)
           .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<BranchSchedule?> FindByWeekDayAsync(byte branchId, WeekDay day)
    {
        var horarioSucursal = await context.Set<BranchSchedule>()
          .AsNoTracking()
          .Include(m => m.ScheduleIdNavigation)
          .Include(v => v.BranchScheduleBlocks)
          .FirstOrDefaultAsync(m => m.BranchId == branchId && m.ScheduleIdNavigation.Day == day);
        return horarioSucursal;
    }

    /// <inheritdoc />
    public async Task<BranchSchedule?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(BranchSchedule))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<BranchSchedule>()
                .Include(m => m.ScheduleIdNavigation)
                .Include(m => m.BranchIdNavigation)
                .Include(m => m.BranchScheduleBlocks)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Remove all the existings branch schedule blocks
    /// </summary>
    /// <param name="bloqueosExistentes">List of branch schedule blocks to be removed</param>
    private void RemoveBlocks(IEnumerable<ICollection<BranchScheduleBlock>> bloqueosExistentes)
    {
        foreach (var horarioExistente in bloqueosExistentes)
        {
            context.BranchScheduleBlocks.RemoveRange(horarioExistente);
        }
    }

    /// <summary>
    /// Reorganize branch schedule blocks
    /// </summary>
    /// <param name="sucursalHorariosExistentes">List of existing branch schedule blocks</param>
    /// <param name="sucursalHorarios">List of the new branch schedules that will be receiving existing branch schedules blocks</param>
    private static void ReorganizeBlocks(ICollection<BranchSchedule> sucursalHorariosExistentes, IEnumerable<BranchSchedule> branchSchedules)
    {
        foreach (var item in branchSchedules)
        {
            var existing = sucursalHorariosExistentes.SingleOrDefault(m => m.BranchId == item.BranchId && m.ScheduleId == item.ScheduleId);
            if (existing != null && existing.BranchScheduleBlocks.Any())
            {
                var listaBloqueos = existing.BranchScheduleBlocks.ToList();
                listaBloqueos.ForEach(m => m.Id = 0);
                item.BranchScheduleBlocks = listaBloqueos;
            }
        }
    }
}