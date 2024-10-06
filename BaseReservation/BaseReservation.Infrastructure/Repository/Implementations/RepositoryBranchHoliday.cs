using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryBranchHoliday(BaseReservationContext context) : IRepositoryBranchHoliday
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchHolidaysAsync(byte branchId, IEnumerable<BranchHoliday> branchHolidays)
    {
        var result = true;
        var feriadosExistentes = await ListAllByBranchAsync(branchId);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.BranchHolidays.RemoveRange(feriadosExistentes);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && feriadosExistentes.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.BranchHolidays.AddRange(branchHolidays);
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
    public async Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<BranchHoliday>()
                .AsNoTracking()
                .Include(m => m.HolidayIdNavigation)
                .Where(m => m.BranchId == branchId)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId, short year)
    {
        var collection = await context.Set<BranchHoliday>()
                .AsNoTracking()
                .Include(m => m.HolidayIdNavigation)
                .Where(m => m.BranchId == branchId && m.Year == year)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId, DateOnly startDate, DateOnly endDate)
    {
        var collection = await context.Set<BranchHoliday>()
                .AsNoTracking()
                .Include(m => m.HolidayIdNavigation)
                .Where(m => m.BranchId == branchId && m.Date >= startDate && m.Date <= endDate)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<BranchHoliday?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(BranchHoliday))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<BranchHoliday>()
                .Include(m => m.HolidayIdNavigation)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }
}