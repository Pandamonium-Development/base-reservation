using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryFeriado(BaseReservationContext context) : IRepositoryFeriado
{
    /// <summary>
    /// Create a new Holyday
    /// </summary>
    /// <param name="feriado"> Holyday model to be add </param>
    /// <returns>Feriado</returns>
    public async Task<Feriado> CreateFeriadoAsync(Feriado feriado)
    {
        var result = context.Feriados.Add(feriado);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Delete a Holiday 
    /// </summary>
    /// <param name="id"> The unique identifier of the Holiday model is removed</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    public async Task<bool> DeleteFeriadoAsync(byte id)
    {
        var feriado = await FindByIdAsync(id);
        feriado!.Activo = false;

        context.Feriados.Update(feriado);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <summary>
    /// Checks if a Holiday with the specified identifier exists and is active.    
    /// /// </summary>
    /// <param name="id">The unique identifier of the Holiday to check for existence. </param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsFeriadoAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Feriado))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Feriado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <summary>
    /// Finds an active Holiday by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Holiday.</param>
    /// <returns>Feriado if founded, otherwise null</returns>
    public async Task<Feriado?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Feriado))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Feriado>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <summary>
    /// List all active Holidays.
    /// </summary>
    /// <returns>ICollection of Feriado</returns>
    public async Task<ICollection<Feriado>> ListAllAsync()
    {
        var collection = await context.Set<Feriado>()
                .Where(m => m.Activo)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Updates an existing Holiday.
    /// </summary>
    /// <param name="feriado">The Holiday entity to update. </param>
    /// <returns>Feriado</returns>
    public async Task<Feriado> UpdateFeriadoAsync(Feriado feriado)
    {
        context.Feriados.Update(feriado);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(feriado.Id);
        return response!;
    }
}