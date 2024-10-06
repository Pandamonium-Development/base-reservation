using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryPaymentType
{
    /// <summary>
    /// Get list of all types of payment
    /// </summary>
    /// <returns>ICollection of PaymentType</returns>
    Task<ICollection<PaymentType>> ListAllAsync();
}