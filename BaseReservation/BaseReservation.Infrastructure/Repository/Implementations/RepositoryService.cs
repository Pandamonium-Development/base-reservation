using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryService(BaseReservationContext context) : IRepositoryService
{
    /// <inheritdoc />
    public async Task<Service> CreateServiceAsync(Service service)
    {
        var result = context.Services.Add(service);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteServiceAsync(byte id)
    {
        var service = await FindByIdAsync(id);
        service!.Active = false;

        context.Services.Update(service);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsServiceAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Service))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Service>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<Service?> FindByIdAsync(byte id) => await context.Set<Service>().FindAsync(id);

    /// <inheritdoc />
    public async Task<ICollection<Service>> ListAllAsync()
    {
        var collection = await context.Set<Service>()
       .Include(a => a.TypeServiceIdNavigation)
       .AsNoTracking()
       .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Service> UpdateServiceAsync(Service service)
    {
        context.Services.Update(service);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(service.Id);
        return response!;
    }
}