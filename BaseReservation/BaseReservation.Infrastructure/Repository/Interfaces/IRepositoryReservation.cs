using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReservation
{
    /// <summary>
    /// Create reservation
    /// </summary>
    /// <param name="reservation">Reservation model to be added</param>
    /// <returns>Reservation</returns>
    Task<Reservation> CreateReservationAsync(Reservation reservation);

    /// <summary>
    /// Update reservation
    /// </summary>
    /// <param name="reservation">Reservation model to be updated</param>
    /// <returns>Reservation</returns>
    Task<Reservation> UpdateReservationAsync(Reservation reservation);

    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns>ICollection of Reservation</returns>
    Task<ICollection<Reservation>> ListAllAsync();

    /// <summary>
    /// Get list of all reservations by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of Reservation</returns>
    Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Get list of all reservations by branch, date start and end date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">En date</param>
    /// <returns>ICollection of Reservation</returns>
    Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId, DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Get list of all reservations by branch and date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="day">Date to look for</param>
    /// <returns>ICollection of Reservation</returns>
    Task<ICollection<Reservation>> ListAllByBranchAsync(byte branchId, DateOnly day);

    /// <summary>
    /// Get reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Reservation if founded, otherwise null</returns>
    Task<Reservation?> FindByIdAsync(int id);

    /// <summary>
    /// Validate if exists reservation
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsReservationAsync(int id);
}