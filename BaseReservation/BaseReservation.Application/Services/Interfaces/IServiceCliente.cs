using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceCliente
{
    /// <summary>
    /// Get list of all existing customer.
    /// </summary>
    /// <returns>ICollection of ResponseClienteDto.</returns>
    Task<ICollection<ResponseClienteDto>> ListAllAsync();

    /// <summary>
    ///  Get exact customer according to id, if not, get null
    /// </summary>
    /// <param name="id">The ID of the customer.</param>
    /// <returns>ResponseClienteDto</returns>
    Task<ResponseClienteDto?> FindByIdAsync(short id);

    /// <summary>
    /// Deletes a customer based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the customer to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteCustomerAsync(short id);
}