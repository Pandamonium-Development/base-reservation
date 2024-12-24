using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInvoiceDetail
{
    /// <summary>
    /// Get exact invoice detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>InvoiceDetail</returns>
    Task<InvoiceDetail?> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all existing invoice details according to a parent invoice
    /// </summary>
    /// <param name="invoiceId">Id Invoice parent</param>
    /// <returns>ICollection of InvoiceDetail</returns>
    Task<ICollection<InvoiceDetail>> ListAllByInvoiceAsync(long invoiceId);
}