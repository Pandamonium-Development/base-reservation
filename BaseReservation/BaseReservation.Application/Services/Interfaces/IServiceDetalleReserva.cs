using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetalleReserva
{
    /// <summary>
    /// Get list of all reservation details by branch
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <returns>ICollection of ResponseDetalleReservaDto</returns>
    Task<ICollection<ResponseDetalleReservaDto>> ListAllByReservaAsync(int idReserva);

    /// <summary>
    /// Get reservation detail with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseDetalleReservaDto</returns>
    Task<ResponseDetalleReservaDto?> FindByIdAsync(int id);

    /// <summary>
    /// Create reservation details
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <param name="detallesReserva">List of reservation detail model to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<RequestDetalleReservaDto> detallesReserva);
}