
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCliente(BaseReservationContext context) : IRepositoryCliente
{
    /// <inheritdoc />
    public async Task<ICollection<Cliente>> ListAllAsync()
    {
        var collection = await context.Set<Cliente>()
                .Where(m => m.Activo)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(short id)
    {
        var customer = await FindByIdAsync(id);
        customer!.Activo = false;

        context.Clientes.Update(customer);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsCustomerAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Cliente))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Cliente>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <inheritdoc />
    public async Task<Cliente?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Cliente))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Cliente>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <inheritdoc />
    public async Task<Cliente> UpdateCostumerAsync(Cliente customer)
    {
        context.Clientes.Update(customer);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(customer.Id);
        return response!;
    }
}