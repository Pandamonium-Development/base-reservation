using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReservationQuestion
{
    /// <summary>
    /// Get list of all questions reservation
    /// </summary>
    /// <returns>ICollection of ReservationQuestion</returns>
    Task<ICollection<ReservationQuestion>> ListAllAsync();

    /// <summary>
    /// Get reservation's question with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ReservationQuestion if founded, it not, false</returns>
    Task<ReservationQuestion?> FindByIdAsync(int id);
}