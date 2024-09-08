using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDetallePedido
{
    /// <summary>
    /// Get exact order detail according to id, if not, get null
    /// </summary>
    /// <param name="id">Id number</param>
    /// <returns>DetallePedido</returns>
    Task<DetallePedido?> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all existing order details according to a parent order
    /// </summary>
    /// <param name="idPedido">Id Order parent</param>
    /// <returns>ICollection of DetallePedido</returns>
    Task<ICollection<DetallePedido>> ListAllByPedidoAsync(long idPedido);
}