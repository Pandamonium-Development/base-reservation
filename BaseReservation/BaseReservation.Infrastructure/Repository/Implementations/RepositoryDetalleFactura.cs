using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDetalleFactura(BaseReservationContext context) : IRepositoryDetalleFactura
{
    /// <summary>
    /// Get exact invoice detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>DetalleFactura</returns>
    public async Task<DetalleFactura?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(DetalleFactura))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<DetalleFactura>()
            .Include(a => a.IdFacturaNavigation)
            .Include(a => a.IdServicioNavigation)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get list of all existing invoice details according to a parent invoice
    /// </summary>
    /// <param name="idFactura">Id Invoice parent</param>
    /// <returns>ICollection of Detalle Factura</returns>
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