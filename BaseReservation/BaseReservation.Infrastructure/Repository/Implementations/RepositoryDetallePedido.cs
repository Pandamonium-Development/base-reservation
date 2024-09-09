using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDetallePedido(BaseReservationContext context) : IRepositoryDetallePedido
{
    /// <inheritdoc />
    public async Task<DetallePedido?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetallePedido))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetallePedido>()
            .Include(a => a.IdPedidoNavigation)
            .Include(a => a.IdServicioNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
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