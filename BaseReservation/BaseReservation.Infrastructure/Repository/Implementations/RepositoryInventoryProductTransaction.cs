using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventoryProductTransaction(BaseReservationContext context, IRepositoryInventoryProduct repositoryInventoryProduct) : IRepositoryInventoryProductTransaction
{
    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductTransactionAsync(InventoryProductTransaction inventoryProductTransaction)
    {
        var result = true;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.InventoryProductTransactions.Add(inventoryProductTransaction);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    var inventoryProduct = await repositoryInventoryProduct.FindByIdAsync(inventoryProductTransaction.InventoryProductId);
                    inventoryProduct!.Assignable += inventoryProductTransaction.TransactionType == Enums.TransactionTypeInventory.Salida ? inventoryProductTransaction.Quantity * -1 : inventoryProductTransaction.Quantity * 1;

                    context.InventoryProducts.Update(inventoryProduct);
                    rowsAffected = await context.SaveChangesAsync();

                    if (rowsAffected == 0)
                    {
                        await transaccion.RollbackAsync();
                        result = false;
                    }
                    else
                    {
                        await transaccion.CommitAsync();
                    }
                }

            }
            catch (Exception exc)
            {
                await transaccion.RollbackAsync();
                throw new RequestFailedException("Error al guardar movimiento inventario", exc);
            }
        });

        return result;
    }

    /// <inheritdoc />
    public async Task<ICollection<InventoryProductTransaction>> ListAllByInventoryAsync(short idInventario)
    {
        var collection = await context.Set<InventoryProductTransaction>()
            .AsNoTracking()
            .Include(m => m.InventoryProductIdNavigation)
            .ThenInclude(m => m.ProductIdNavigation)
            .Include(m => m.InventoryProductIdNavigation)
            .ThenInclude(m => m.InventoryIdNavigation)
            .Where(m => m.InventoryProductIdNavigation.InventoryId == idInventario)
            .AsNoTracking()
            .ToListAsync();

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<InventoryProductTransaction>> ListAllByProductAsync(short idProducto)
    {
        var collection = await context.Set<InventoryProductTransaction>()
            .AsNoTracking()
            .Include(m => m.InventoryProductIdNavigation)
            .ThenInclude(m => m.ProductIdNavigation)
            .Include(m => m.InventoryProductIdNavigation)
            .ThenInclude(m => m.InventoryIdNavigation)
            .Where(m => m.InventoryProductIdNavigation.ProductId == idProducto)
            .AsNoTracking()
            .ToListAsync();

        return collection;
    }
}