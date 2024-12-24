using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInvoice
{
    /// <summary>
    /// Create a new invoice
    /// </summary>
    /// <param name="invoice">Invoice model to be add</param>
    /// <param name="order">Order to be updated if invoice comes from an order</param>
    /// <returns>Invoice</returns>
    Task<Invoice> CreateAsync(Invoice invoice, Order? order);

    /// <summary>
    /// Get list of all existing invoices
    /// </summary>
    /// <returns>ICollection of Invoice</returns>
    Task<ICollection<Invoice>> ListAllAsync();

    /// <summary>
    /// Get exact invoice according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>Invoice</returns>
    Task<Invoice?> FindByIdAsync(long id);
}