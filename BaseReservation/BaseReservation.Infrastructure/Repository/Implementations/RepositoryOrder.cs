using System.Data.Common;
using System.Diagnostics;
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryOrder(BaseReservationContext context) : IRepositoryOrder
{
    /// <inheritdoc />
    public async Task<Order> CreateOrderAsync(Order order, Reservation reservation)
    {
        Order result = null!;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                var tracking = context.Orders.Add(order);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    await transaccion.RollbackAsync();
                    throw (new Exception("No se ha podido guardar el pedido.") as SqlException)!;
                }

                context.Reservations.Update(reservation);

                rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    await transaccion.RollbackAsync();
                    throw (new Exception("No se ha podido actualizar la reserva.") as SqlException)!;
                }

                result = tracking.Entity;
                await transaccion.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                throw new RequestFailedException(ex.Message, ex);
            }

        });

        return result;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsOrderAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Order))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Order>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<Order?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Order))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Order>()
            .Include(a => a.CustomerIdNavigation)
            .Include(a => a.PaymentTypeIdNavigation)
            .Include(a => a.TaxIdNavigation)
            .Include(a => a.BranchIdNavigation)
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.ServiceIdNavigation!) /*TO DO*/
            .ThenInclude(a => a.TypeServiceIdNavigation)
            .Include(a => a.OrderDetails)
            .ThenInclude(a => a.OrderDetailProducts)
            .ThenInclude(a => a.ProductIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Order>> ListAllAsync()
    {
        var collection = await context.Set<Order>()
            .Include(a => a.PaymentTypeIdNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}