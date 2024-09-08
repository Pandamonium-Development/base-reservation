using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDetalleFactura(BaseReservationContext context) : IRepositoryDetalleFactura
{
    /// <inheritdoc />
    public async Task<DetalleFactura?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetalleFactura))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetalleFactura>()
            .Include(a => a.IdFacturaNavigation)
            .Include(a => a.IdServicioNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<DetalleFactura>> ListAllByFacturaAsync(long idFactura)
    {
        var collection = await context.Set<DetalleFactura>()
            .Include(a => a.IdFacturaNavigation)
            .Include(a => a.IdServicioNavigation)
            .Where(a => a.IdFactura == idFactura)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}