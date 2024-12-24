using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryHoliday(BaseReservationContext context) : IRepositoryHoliday
{
    /// <inheritdoc />
    public async Task<Holiday> CreateHolidayAsync(Holiday holiday)
    {
        var result = context.Holidays.Add(holiday);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteHolidayAsync(byte id)
    {
        var holiday = await FindByIdAsync(id);
        holiday!.Active = false;

        context.Holidays.Update(holiday);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsHolidayAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Holiday))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Holiday>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active) != null;
    }

    /// <inheritdoc />
    public async Task<Holiday?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Holiday))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Holiday>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active);
    }

    /// <inheritdoc />
    public async Task<ICollection<Holiday>> ListAllAsync()
    {
        var collection = await context.Set<Holiday>()
                .Where(m => m.Active)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Holiday> UpdateHolidayAsync(Holiday holiday)
    {
        context.Holidays.Update(holiday);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(holiday.Id);
        return response!;
    }
}