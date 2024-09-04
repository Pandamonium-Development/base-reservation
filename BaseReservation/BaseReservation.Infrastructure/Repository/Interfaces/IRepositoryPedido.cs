using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryPedido
{
    Task<Pedido> CreatePedidoAsync(Pedido pedido, Reserva reserva);

    Task<ICollection<Pedido>> ListAllAsync();

    Task<Pedido?> FindByIdAsync(long id);

    Task<bool> ExistsPedidoAsync(long id);
}
