using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventarioProductoMovimiento(BaseReservationContext context, IRepositoryInventarioProducto repositoryInventarioProducto) : IRepositoryInventarioProductoMovimiento
{
    public async Task<bool> AgregarInventarioMovimientoProducto(InventarioProductoMovimiento inventarioProductoMovimiento)
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
                    var inventarioProducto = await repositoryInventarioProducto.GetInventarioProductoById(inventarioProductoMovimiento.IdInventarioProducto);
                    inventarioProducto!.Disponible += inventarioProductoMovimiento.TipoMovimiento == Enums.TipoMovimientoInventario.Salida ? inventarioProductoMovimiento.Cantidad * -1 : inventarioProductoMovimiento.Cantidad * 1;

                    context.InventarioProducto.Update(inventarioProducto);
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

    public async Task<ICollection<InventarioProductoMovimiento>> ObtenerMovimientosInventarioByInventario(short idInventario)
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

    public async Task<ICollection<InventarioProductoMovimiento>> ObtenerMovimientosInventarioByProducto(short idProducto)
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
