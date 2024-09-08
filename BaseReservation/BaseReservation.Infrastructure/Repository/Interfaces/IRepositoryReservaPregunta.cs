using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReservaPregunta
{
    /// <summary>
    /// Get list of all questions reservation
    /// </summary>
    /// <returns>ICollection of ReservaPregunta</returns>
    Task<ICollection<ReservaPregunta>> ListAllAsync();

    /// <summary>
    /// Get reservation's question with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ReservaPregunta if founded, it not, false</returns>
    Task<ReservaPregunta?> FindByIdAsync(int id);
}