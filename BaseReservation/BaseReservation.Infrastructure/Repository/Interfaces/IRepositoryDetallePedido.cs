using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDetallePedido
{
    Task<DetallePedido?> FindByIdAsync(long id);

    Task<ICollection<DetallePedido>> ListAllByPedidoAsync(long idPedido);
}
