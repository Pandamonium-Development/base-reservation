using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReservationDetail
{
    /// <summary>
    /// Get list of all reservation details by branch
    /// </summary>
    /// <param name="reservationId">Branch id</param>
    /// <returns>ICollection of ResponseReservationDetailDto</returns>
    Task<ICollection<ResponseReservationDetailDto>> ListAllByReservationAsync(int reservationId);

    /// <summary>
    /// Get reservation detail with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseReservationDetailDto</returns>
    Task<ResponseReservationDetailDto?> FindByIdAsync(int id);

    /// <summary>
    /// Create reservation details
    /// </summary>
    /// <param name="reservationId">Branch id</param>
    /// <param name="reservationDetails">List of reservation detail model to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateReservationDetailAsync(int reservationId, IEnumerable<RequestReservationDetailDto> reservationDetails);
}