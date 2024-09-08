using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryImpuesto
{
    /// <summary>
    /// Lists all Tax entities
    /// </summary>
    /// <returns>ICollection of Impuesto</returns>
    Task<ICollection<Impuesto>> ListAllAsync();
}