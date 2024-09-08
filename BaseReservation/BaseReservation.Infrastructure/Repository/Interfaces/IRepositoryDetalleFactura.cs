using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDetalleFactura
{
    /// <summary>
    /// Get exact invoice detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>DetalleFactura</returns>
    Task<DetalleFactura?> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all existing invoice details according to a parent invoice
    /// </summary>
    /// <param name="idFactura">Id Invoice parent</param>
    /// <returns>ICollection of Detalle Factura</returns>
    Task<ICollection<DetalleFactura>> ListAllByFacturaAsync(long idFactura);
}