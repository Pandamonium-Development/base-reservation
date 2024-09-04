using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDetallePedido(BaseReservationContext context) : IRepositoryDetallePedido
{
    /// <summary>
    /// Get exact order detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>DetallePedido</returns>
    public async Task<DetallePedido?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetallePedido))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetallePedido>()
            .Include(a => a.IdPedidoNavigation)
            .Include(a => a.IdServicioNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all existing order details according to a parent order
    /// </summary>
    /// <param name="idPedido">Id Order parent</param>
    /// <returns>ICollection of DetallePedido</returns>
    public async Task<ICollection<DetallePedido>> ListAllByPedidoAsync(long idPedido)
    {
        var collection = await context.Set<DetallePedido>()
            .Include(a => a.IdPedidoNavigation)
            .Include(a => a.IdServicioNavigation)
            .Where(a => a.IdPedido == idPedido)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}