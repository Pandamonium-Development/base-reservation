using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventarioProductoMovimiento(BaseReservationContext context, IRepositoryInventarioProducto repositoryInventarioProducto) : IRepositoryInventarioProductoMovimiento
{
    /// <summary>
    /// Creates a new InventarioProductoMovimiento and updates the related InventarioProducto based on the movement type.
    /// </summary>
    /// <param name="inventarioProductoMovimiento">The InventarioProductoMovimiento entity to be added.</param>
    /// <returns>InventarioProductoMovimiento</returns>
    /// <exception cref="RequestFailedException"></exception>
    public async Task<bool> CreateInventarioMovimientoProductoAsync(InventarioProductoMovimiento inventarioProductoMovimiento)
    {
        var result = true;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.InventarioProductoMovimientos.Add(inventarioProductoMovimiento);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    var inventarioProducto = await repositoryInventarioProducto.FindByIdAsync(inventarioProductoMovimiento.IdInventarioProducto);
                    inventarioProducto!.Disponible += inventarioProductoMovimiento.TipoMovimiento == Enums.TipoMovimientoInventario.Salida ? inventarioProductoMovimiento.Cantidad * -1 : inventarioProductoMovimiento.Cantidad * 1;

                    context.InventarioProductos.Update(inventarioProducto);
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

    /// <summary>
    /// Lists all InventarioProductoMovimiento entities associated with a specific Inventario.
    /// </summary>
    /// <param name="idInventario"> The unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventarioProductoMovimiento</returns>
    public async Task<ICollection<InventarioProductoMovimiento>> ListAllByInventarioAsync(short idInventario)
    {
        var collection = await context.Set<InventarioProductoMovimiento>()
            .AsNoTracking()
            .Include(m => m.IdInventarioProductoNavigation)
            .ThenInclude(m => m.IdProductoNavigation)
            .Include(m => m.IdInventarioProductoNavigation)
            .ThenInclude(m => m.IdInventarioNavigation)
            .Where(m => m.IdInventarioProductoNavigation.IdInventario == idInventario)
            .AsNoTracking()
            .ToListAsync();

        return collection;
    }

    /// <summary>
    /// Lists all InventarioProductoMovimiento entities associated with a specific Producto.
    /// </summary>
    /// <param name="idProducto">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventarioProductoMovimiento</returns>
    public async Task<ICollection<InventarioProductoMovimiento>> ListAllByProductoAsync(short idProducto)
    {
        var collection = await context.Set<InventarioProductoMovimiento>()
            .AsNoTracking()
            .Include(m => m.IdInventarioProductoNavigation)
            .ThenInclude(m => m.IdProductoNavigation)
            .Include(m => m.IdInventarioProductoNavigation)
            .ThenInclude(m => m.IdInventarioNavigation)
            .Where(m => m.IdInventarioProductoNavigation.IdProducto == idProducto)
            .AsNoTracking()
            .ToListAsync();

        return collection;
    }
}
