using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDetalleReserva
{
    /// <summary>
    /// Get list of all details reservation
    /// </summary>
    /// <param name="idReserva">Reservation id</param>
    /// <returns>ICollection of DetalleReserva</returns>
    Task<ICollection<DetalleReserva>> ListAllByReservaAsync(int idReserva);

    /// <summary>
    /// Get detail reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>DetalleReserva if founded, otherwise null</returns>
    Task<DetalleReserva?> FindByIdAsync(int id);

    /// <summary>
    /// Create multiple details reservation
    /// </summary>
    /// <param name="idReserva">Reservation id</param>
    /// <param name="detallesReserva">List of detail reservation</param>
    /// <returns>True if were added, if not, false</returns>
    Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<DetalleReserva> detallesReserva);
}