using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInvoiceDetail(BaseReservationContext context) : IRepositoryInvoiceDetail
{
    /// <inheritdoc />
    public async Task<InvoiceDetail?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InvoiceDetail))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<InvoiceDetail>()
            .Include(a => a.InvoiceIdNavigation)
            .Include(a => a.ServiceIdNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<InvoiceDetail>> ListAllByInvoiceAsync(long invoiceId)
    {
        var collection = await context.Set<InvoiceDetail>()
            .Include(a => a.InvoiceIdNavigation)
            .Include(a => a.ServiceIdNavigation)
            .Where(a => a.InvoiceId == invoiceId)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}