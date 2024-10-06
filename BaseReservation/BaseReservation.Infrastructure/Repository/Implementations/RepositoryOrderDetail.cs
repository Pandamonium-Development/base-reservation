using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryOrderDetail(BaseReservationContext context) : IRepositoryOrderDetail
{
    /// <inheritdoc />
    public async Task<OrderDetail?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(OrderDetail))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<OrderDetail>()
            .Include(a => a.OrderIdNavigation)
            .Include(a => a.ServiceIdNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<OrderDetail>> ListAllByOrderAsync(long orderId)
    {
        var collection = await context.Set<OrderDetail>()
            .Include(a => a.OrderIdNavigation)
            .Include(a => a.ServiceIdNavigation)
            .Where(a => a.OrderId == orderId)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}