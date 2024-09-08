using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryFactura
{
    /// <summary>
    /// Create a new invoice
    /// </summary>
    /// <param name="factura">Invoice model to be add</param>
    /// <param name="pedido">Order to be updated if invoice comes from an order</param>
    /// <returns>Factura</returns>
    Task<Factura> CreateAsync(Factura factura, Pedido? pedido);

    /// <summary>
    /// Get list of all existing invoices
    /// </summary>
    /// <returns>ICollection of Factura</returns>
    Task<ICollection<Factura>> ListAllAsync();

    /// <summary>
    /// Get exact invoice according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Factura</returns>
    Task<Factura?> FindByIdAsync(long id);
}