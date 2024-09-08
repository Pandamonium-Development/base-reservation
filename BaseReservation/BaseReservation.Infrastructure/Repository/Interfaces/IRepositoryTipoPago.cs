using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTipoPago
{
    /// <summary>
    /// Get list of all types of payment
    /// </summary>
    /// <returns>ICollection of TipoPago</returns>
    Task<ICollection<TipoPago>> ListAllAsync();
}