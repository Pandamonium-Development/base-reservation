using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryServicio(BaseReservationContext context) : IRepositoryServicio
{
    /// <summary>
    /// Create service
    /// </summary>
    /// <param name="servicio">Service model to be added</param>
    /// <returns>Servicio</returns>
    public async Task<Servicio> CreateServicioAsync(Servicio servicio)
    {
        var result = context.Servicios.Add(servicio);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Validate if exists service
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsServicioAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Servicio))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Servicio>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Get service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Servicio if founded, otherwise null</returns>
    public async Task<Servicio?> FindByIdAsync(byte id)
    {
        return await context.Set<Servicio>().FindAsync(id);
    }

    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>ICollection of Servicio</returns>
    public async Task<ICollection<Servicio>> ListAllAsync()
    {
        var collection = await context.Set<Servicio>()
       .Include(a => a.IdTipoServicioNavigation)
       .AsNoTracking()
       .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Update service
    /// </summary>
    /// <param name="servicio">Service model to be updated</param>
    /// <returns>Servicio</returns>
    public async Task<Servicio> UpdateServicioAsync(Servicio servicio)
    {
        context.Servicios.Update(servicio);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(servicio.Id);
        return response!;
    }
}