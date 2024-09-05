using System.Data.Common;
using System.Diagnostics;
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryPedido(BaseReservationContext context) : IRepositoryPedido
{
    /// <summary>
    /// Creates a new Pedido and updates the associated Reserva within a trasaction. 
    /// If either operation fails, the transaction is rolled back.
    /// </summary>
    /// <param name="pedido">The pedido entity to be created.</param>
    /// <param name="reserva">The reserva entity to be created.</param>
    /// <returns>Pedido</returns>
    /// <exception cref="RequestFailedException"></exception>
    public async Task<Pedido> CreatePedidoAsync(Pedido pedido, Reserva reserva)
    {
        Pedido result = null!;
        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                var tracking = context.Pedidos.Add(pedido);
                var filasAfectadas = await context.SaveChangesAsync();

                if (filasAfectadas == 0)
                {
                    await transaccion.RollbackAsync();
                    throw (new Exception("No se ha podido guardar el pedido.") as SqlException)!;
                }

                context.Reservas.Update(reserva);

                filasAfectadas = await context.SaveChangesAsync();

                if (filasAfectadas == 0)
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

    /// <summary>
    /// Checks if a Pedido with the specified ID exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Pedido</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsPedidoAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Pedido))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Pedido>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Retrieves a Pedido by its ID, including related entities such as Cliente, TipoPago,
    /// Impuesto, Sucursal, and DetallePedidos.
    /// </summary>
    /// <param name="id">The unique identifier of the Pedido.</param>
    /// <returns>Pedido if founded, otherwise null</returns>
    public async Task<Pedido?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Pedido))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Pedido>()
            .Include(a => a.IdClienteNavigation)
            .Include(a => a.IdTipoPagoNavigation)
            .Include(a => a.IdImpuestoNavigation)
            .Include(a => a.IdSucursalNavigation)
            .Include(a => a.DetallePedidos)
            .ThenInclude(a => a.IdServicioNavigation!) /*TO DO*/
            .ThenInclude(a => a.IdTipoServicioNavigation)
            .Include(a => a.DetallePedidos)
            .ThenInclude(a => a.DetallePedidoProductos)
            .ThenInclude(a => a.IdProductoNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }
    /// <summary>
    /// Retrieves all Pedidos with their associated TipoPago entities.
    /// </summary>
    /// <returns>ICollection of Pedido</returns>
    public async Task<ICollection<Pedido>> ListAllAsync()
    {
        var collection = await context.Set<Pedido>()
            .Include(a => a.IdTipoPagoNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}