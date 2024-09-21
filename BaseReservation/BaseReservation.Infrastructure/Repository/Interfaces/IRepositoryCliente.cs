using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCliente
{
    /// <summary>
    /// Get list of all existing customer.
    /// </summary>
    /// <returns>ICollection of Cliente.</returns>
    Task<ICollection<Cliente>> ListAllAsync();

    /// <summary>
    ///  Get exact customer according to id, if not, get null
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <returns>Cliente</returns>
    Task<Cliente?> FindByIdAsync(short id);

    /// <summary>
    /// Updates an existing customer record.
    /// </summary>
    /// <param name="customer">The customer to update.</param>
    /// <returns>Cliente</returns>
    Task<Cliente> UpdateCostumerAsync(Cliente customer);

    /// <summary>
    /// Deletes a customer based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the customer to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteCustomerAsync(short id);

    /// <summary>
    /// Checks if a costumer exists by its ID
    /// </summary>
    /// <param name="id">The Id of the customer.</param>
    /// <returns>True if customer exist, otherwise false.</returns>
    Task<bool> ExistsCustomerAsync(short id);
}