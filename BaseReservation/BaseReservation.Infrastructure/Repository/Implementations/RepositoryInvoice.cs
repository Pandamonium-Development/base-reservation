using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInvoice(BaseReservationContext context) : IRepositoryInvoice
{
    /// <inheritdoc />
    public async Task<Invoice> CreateAsync(Invoice invoice, Order? order)
    {
        Invoice result = null!;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var tracking = context.Invoices.Add(invoice);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    await transaction.RollbackAsync();
                    throw (new Exception("No se ha podido guardar la factura") as SqlException)!;
                }
                if (order != null)
                {
                    context.Orders.Update(order);

                    rowsAffected = await context.SaveChangesAsync();

                    if (rowsAffected == 0)
                    {
                        await transaction.RollbackAsync();
                        throw (new Exception("No se ha podido actualizar la factura") as SqlException)!;
                    }
                }

                result = tracking.Entity;
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new RequestFailedException(ex.Message, ex);
            }

        });

        return result;
    }

    /// <inheritdoc />
    public async Task<Invoice?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Invoice))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Invoice>()
            .Include(a => a.CustomerIdNavigation)
            .Include(a => a.PaymentTypeIdNavigation)
            .Include(a => a.TaxIdNavigation)
            .Include(a => a.BranchIdNavigation)
            .Include(a => a.InvoiceDetails)
            .ThenInclude(a => a.ServiceIdNavigation)
            .Include(a => a.InvoiceDetails)
            .ThenInclude(a => a.InvoiceDetailProducts)
            .ThenInclude(a => a.ProductIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Invoice>> ListAllAsync()
    {
        var collection = await context.Set<Invoice>()
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}