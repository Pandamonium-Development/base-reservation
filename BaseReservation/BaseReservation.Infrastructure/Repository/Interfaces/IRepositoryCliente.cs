using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCliente
{
    /// <summary>
    /// Get list of all existing customer
    /// </summary>
    /// <returns>ICollection of Categoria</returns>
    Task<ICollection<Cliente>> ListAllAsync();
}