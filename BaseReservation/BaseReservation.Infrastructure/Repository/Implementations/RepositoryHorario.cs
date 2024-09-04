using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryHorario(BaseReservationContext context) : IRepositoryHorario
{
    /// <summary>
    /// Creates a new Schedule
    /// </summary>
    /// <param name="horario">The Schedule entity to be added.</param>
    /// <returns></returns>
    public async Task<Horario> CreateHorarioAsync(Horario horario)
    {
        var result = context.Horarios.Add(horario);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Updates an existing Schedule.
    /// </summary>
    /// <param name="horario">The Shedule entity to update.</param>
    /// <returns></returns>
    public async Task<Horario> UpdateHorarioAsync(Horario horario)
    {
        context.Horarios.Update(horario);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(horario.Id);
        return response!;
    }

    /// <summary>
    /// Finds a Schedule by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule.</param>
    /// <returns></returns>
    public async Task<Horario?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Horario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Horario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Lists all Schedules.
    /// </summary>
    /// <returns></returns>
    public async Task<ICollection<Horario>> ListAllAsync()
    {
        var collection = await context.Set<Horario>()
               .AsNoTracking()
               .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Checks if a Schedule with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule</param>
    /// <returns></returns>
    public async Task<bool> ExistsHorarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Horario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Horario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }
}