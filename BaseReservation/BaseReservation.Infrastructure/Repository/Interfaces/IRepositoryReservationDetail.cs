using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReservationDetail
{
    /// <summary>
    /// Get list of all details reservation
    /// </summary>
    /// <param name="reservationId">Reservation id</param>
    /// <returns>ICollection of ReservationDetail</returns>
    Task<ICollection<ReservationDetail>> ListAllByReservationAsync(int reservationId);

    /// <summary>
    /// Get detail reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ReservationDetail if founded, otherwise null</returns>
    Task<ReservationDetail?> FindByIdAsync(int id);

    /// <summary>
    /// Create multiple details reservation
    /// </summary>
    /// <param name="reservationId">Reservation id</param>
    /// <param name="reservationDetails">List of detail reservation</param>
    /// <returns>True if were added, if not, false</returns>
    Task<bool> CreateReservationDetailAsync(int reservationId, IEnumerable<ReservationDetail> reservationDetails);
}