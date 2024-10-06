
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCustomer(BaseReservationContext context) : IRepositoryCustomer
{
    /// <inheritdoc />
    public async Task<ICollection<Customer>> ListAllAsync()
    {
        var collection = await context.Set<Customer>()
                .Where(m => m.Active)
                .AsNoTracking()
                .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCustomerAsync(short id)
    {
        var customer = await FindByIdAsync(id);
        customer!.Active = false;

        context.Customers.Update(customer);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsCustomerAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Customer))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Customer>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active) != null;
    }

    /// <inheritdoc />
    public async Task<Customer?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Customer))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Customer>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active);
    }

    /// <inheritdoc />
    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        context.Customers.Update(customer);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(customer.Id);
        return response!;
    }
}