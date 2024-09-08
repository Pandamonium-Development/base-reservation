using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryFactura(BaseReservationContext context) : IRepositoryFactura
{
    /// <inheritdoc />
    public async Task<Factura> CreateAsync(Factura factura, Pedido? pedido)
    {
        Factura result = null!;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                var tracking = context.Facturas.Add(factura);
                var filasAfectadas = await context.SaveChangesAsync();

                if (filasAfectadas == 0)
                {
                    await transaccion.RollbackAsync();
                    throw (new Exception("No se ha podido guardar la factura") as SqlException)!;
                }
                if (pedido != null)
                {
                    context.Pedidos.Update(pedido);

                    filasAfectadas = await context.SaveChangesAsync();

                    if (filasAfectadas == 0)
                    {
                        await transaccion.RollbackAsync();
                        throw (new Exception("No se ha podido actualizar la factura") as SqlException)!;
                    }
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
    public async Task<Factura?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Factura))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Factura>()
            .Include(a => a.IdClienteNavigation)
            .Include(a => a.IdTipoPagoNavigation)
            .Include(a => a.IdImpuestoNavigation)
            .Include(a => a.IdSucursalNavigation)
            .Include(a => a.DetalleFacturas)
            .ThenInclude(a => a.IdServicioNavigation)
            .Include(a => a.DetalleFacturas)
            .ThenInclude(a => a.DetalleFacturaProductos)
            .ThenInclude(a => a.IdProductoNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Factura>> ListAllAsync()
    {
        var collection = await context.Set<Factura>()
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}