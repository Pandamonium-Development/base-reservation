using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryPedido
{
    /// <summary>
    /// Creates a new Pedido and updates the associated Reserva within a trasaction. 
    /// If either operation fails, the transaction is rolled back.
    /// </summary>
    /// <param name="pedido">The pedido entity to be created.</param>
    /// <param name="reserva">The reserva entity to be created.</param>
    /// <returns>Pedido</returns>
    /// <exception cref="RequestFailedException"></exception>
    Task<Pedido> CreatePedidoAsync(Pedido pedido, Reserva reserva);

    /// <summary>
    /// Retrieves all Pedidos with their associated TipoPago entities.
    /// </summary>
    /// <returns>ICollection of Pedido</returns>    
    Task<ICollection<Pedido>> ListAllAsync();

    /// <summary>
    /// Retrieves a Pedido by its ID, including related entities such as Cliente, TipoPago,
    /// Impuesto, Sucursal, and DetallePedidos.
    /// </summary>
    /// <param name="id">The unique identifier of the Pedido.</param>
    /// <returns>Pedido if founded, otherwise null</returns>
    Task<Pedido?> FindByIdAsync(long id);

    /// <summary>
    /// Checks if a Pedido with the specified ID exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Pedido</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsPedidoAsync(long id);
}