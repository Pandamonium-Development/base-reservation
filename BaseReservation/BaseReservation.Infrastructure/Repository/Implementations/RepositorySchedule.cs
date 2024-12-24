using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySchedule(BaseReservationContext context) : IRepositorySchedule
{
    /// <inheritdoc />
    public async Task<Schedule> CreateScheduleAsync(Schedule schedule)
    {
        var result = context.Schedules.Add(schedule);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
    {
        context.Schedules.Update(schedule);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(schedule.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Schedule?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Schedule))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Schedule>()
            .Include(x => x.BranchSchedules)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active);
    }

    /// <inheritdoc />
    public async Task<ICollection<Schedule>> ListAllAsync()
    {
        var collection = await context.Set<Schedule>()
               .AsNoTracking()
               .Where(m => m.Active)
               .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsScheduleAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Schedule))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Schedule>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active) != null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteScheduleAsync(short id)
    {
        var schedule = await FindByIdAsync(id);
        schedule!.Active = false;

        context.Schedules.Update(schedule);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }
}