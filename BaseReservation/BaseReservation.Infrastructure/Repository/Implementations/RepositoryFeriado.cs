using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryFeriado(BaseReservationContext context) : IRepositoryFeriado
{
    /// <inheritdoc />
    public async Task<Feriado> CreateFeriadoAsync(Feriado feriado)
    {
        var result = context.Feriados.Add(feriado);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteFeriadoAsync(byte id)
    {
        var feriado = await FindByIdAsync(id);
        feriado!.Activo = false;

        context.Feriados.Update(feriado);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsFeriadoAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Feriado))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Feriado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <inheritdoc />
    public async Task<Feriado?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Feriado))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Feriado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <inheritdoc />
    public async Task<ICollection<Feriado>> ListAllAsync()
    {
        var collection = await context.Set<Feriado>()
                .Where(m => m.Activo)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Feriado> UpdateFeriadoAsync(Feriado feriado)
    {
        context.Feriados.Update(feriado);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(feriado.Id);
        return response!;
    }
}