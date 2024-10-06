using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTax
{
    /// <summary>
    /// Lists all Tax entities
    /// </summary>
    /// <returns>ICollection of Tax</returns>
    Task<ICollection<Tax>> ListAllAsync();
}