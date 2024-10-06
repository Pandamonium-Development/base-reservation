using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryOrder
{
    /// <summary>
    /// Creates a new Order and updates the associated Reservation within a trasaction. 
    /// If either operation fails, the transaction is rolled back.
    /// </summary>
    /// <param name="order">The order entity to be created.</param>
    /// <param name="reservation">The reservation entity to be created.</param>
    /// <returns>Order</returns>
    /// <exception cref="RequestFailedException"></exception>
    Task<Order> CreateOrderAsync(Order order, Reservation reservation);

    /// <summary>
    /// Retrieves all Orders with their associated TipoPago entities.
    /// </summary>
    /// <returns>ICollection of Order</returns>    
    Task<ICollection<Order>> ListAllAsync();

    /// <summary>
    /// Retrieves a Order by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the Order.</param>
    /// <returns>Order if founded, otherwise null</returns>
    Task<Order?> FindByIdAsync(long id);

    /// <summary>
    /// Checks if a Order with the specified ID exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Order</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsOrderAsync(long id);
}