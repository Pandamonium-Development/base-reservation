using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryServicio(BaseReservationContext context) : IRepositoryServicio
{
    /// <inheritdoc />
    public async Task<Servicio> CreateServiceAsync(Servicio servicio)
    {
        var result = context.Servicios.Add(servicio);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteServiceAsync(byte id)
    {
        var service = await FindByIdAsync(id);
        service!.Activo = false;

        context.Servicios.Update(service);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsServiceAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Servicio))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Servicio>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<Servicio?> FindByIdAsync(byte id)
    {
        return await context.Set<Servicio>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Servicio>> ListAllAsync()
    {
        var collection = await context.Set<Servicio>()
       .Include(a => a.IdTipoServicioNavigation)
       .AsNoTracking()
       .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Servicio> UpdateServiceAsync(Servicio servicio)
    {
        context.Servicios.Update(servicio);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(servicio.Id);
        return response!;
    }
}