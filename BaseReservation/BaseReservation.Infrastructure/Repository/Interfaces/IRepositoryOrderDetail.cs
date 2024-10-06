using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryOrderDetail
{
    /// <summary>
    /// Get exact order detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>OrderDetail</returns>
    Task<OrderDetail?> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all existing order details according to a parent order
    /// </summary>
    /// <param name="orderId">Id Order parent</param>
    /// <returns>ICollection of OrderDetail</returns>
    Task<ICollection<OrderDetail>> ListAllByOrderAsync(long orderId);
}