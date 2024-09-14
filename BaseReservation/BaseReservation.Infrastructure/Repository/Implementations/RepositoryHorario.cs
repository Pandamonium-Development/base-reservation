using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryHorario(BaseReservationContext context) : IRepositoryHorario
{
    /// <inheritdoc />
    public async Task<Horario> CreateHorarioAsync(Horario horario)
    {
        var result = context.Horarios.Add(horario);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Horario> UpdateHorarioAsync(Horario horario)
    {
        context.Horarios.Update(horario);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(horario.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Horario?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Horario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Horario>()
            .Include(x => x.SucursalHorarios)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Horario>> ListAllAsync()
    {
        var collection = await context.Set<Horario>()
               .AsNoTracking()
               .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsHorarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Horario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Horario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteHorarioAsync(short id)
    {
        var schedule = await FindByIdAsync(id);
        schedule!.Activo = false;

        context.Horarios.Update(schedule);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }
}