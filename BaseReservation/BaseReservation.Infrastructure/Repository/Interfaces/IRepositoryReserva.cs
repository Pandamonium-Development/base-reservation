using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReserva
{
    /// <summary>
    /// Create reservation
    /// </summary>
    /// <param name="reserva">Reservation model to be added</param>
    /// <returns>Reserva</returns>
    Task<Reserva> CreateReservaAsync(Reserva reserva);

    /// <summary>
    /// Update reservation
    /// </summary>
    /// <param name="reserva">Reservation model to be updated</param>
    /// <returns>Reserva</returns>
    Task<Reserva> UpdateReservaAsync(Reserva reserva);

    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns>ICollection of Reserva</returns>
    Task<ICollection<Reserva>> ListAllAsync();

    /// <summary>
    /// Get list of all reservations by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of Reserva</returns>
    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Get list of all reservations by branch, date start and end date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="fechaInicio">Start date</param>
    /// <param name="fechaFin">En date</param>
    /// <returns>ICollection of Reserva</returns>
    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin);

    /// <summary>
    /// Get list of all reservations by branch and date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="dia">Date to look for</param>
    /// <returns>ICollection of Reserva</returns>
    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly dia);

    /// <summary>
    /// Get reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Reserva if founded, otherwise null</returns>
    Task<Reserva?> FindByIdAsync(int id);

    /// <summary>
    /// Validate if exists reservation
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsReservaAsync(int id);
}