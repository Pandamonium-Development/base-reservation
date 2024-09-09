using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReserva
{
    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns>ICollection of ResponseReservaDto</returns>
    Task<ICollection<ResponseReservaDto>> ListAllAsync();

    /// <summary>
    /// Get list of all reservations by branch in agenda mode 
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>ICollection of ResponseAgendaCalendarioReservaDto</returns>
    Task<ICollection<ResponseAgendaCalendarioReservaDto>> ListAllBySucursalAsync(byte idSucursal, DateOnly? startDate, DateOnly? endDate);

    /// <summary>
    /// Get reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseReservaDto</returns>
    Task<ResponseReservaDto> FindByIdAsync(int id);

    /// <summary>
    /// Create reservation
    /// </summary>
    /// <param name="reservaDTO">Reservation request model to be added</param>
    /// <returns>ResponseReservaDto</returns>
    Task<ResponseReservaDto> CreateReservaAsync(RequestReservaDto reservaDTO);

    /// <summary>
    /// Update reservation
    /// </summary>
    /// <param name="id">Reservation id</param>
    /// <param name="reservaDTO">Reservation request model to be updated</param>
    /// <returns>ResponseReservaDto</returns>
    Task<ResponseReservaDto> UpdateReservaAsync(int id, RequestReservaDto reservaDTO);

    /// <summary>
    /// Get list of all reservations by branch and week day
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="date">Date to filter</param>
    /// <returns>ICollection of ResponseReservaDto</returns>
    Task<ICollection<ResponseReservaDto>> ListAllBySucursalDiaAsync(byte idSucursal, DateOnly date);

    /// <summary>
    /// Get list of times with schedules availabilities base on branch and date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="date">Date filter</param>
    /// <returns>ICollection of TimeOnly</returns>
    Task<ICollection<TimeOnly>> ScheduleAvailabilityBySucursalAsync(byte idSucursal, DateOnly date);
}