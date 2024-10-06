﻿using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReservation
{
    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns>ICollection of ResponseReservationDto</returns>
    Task<ICollection<ResponseReservationDto>> ListAllAsync();

    /// <summary>
    /// Get list of all reservations by branch in agenda mode 
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>ICollection of ResponseAgendaCalendarioReservationDto</returns>
    Task<ICollection<ResponseReservationCalendarAgendaDto>> ListAllByBranchAsync(byte branchId, DateOnly? startDate, DateOnly? endDate);

    /// <summary>
    /// Get list of all reservations by branch and week day
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="date">Date to filter</param>
    /// <returns>ICollection of ResponseReservationDto</returns>
    Task<ICollection<ResponseReservationDto>> ListAllByBranchAsync(byte branchId, DateOnly date);

    /// <summary>
    /// Get reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseReservationDto</returns>
    Task<ResponseReservationDto> FindByIdAsync(int id);

    /// <summary>
    /// Validate if exists reservation
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsReservationAsync(int id);

    /// <summary>
    /// Create reservation
    /// </summary>
    /// <param name="reservationDTO">Reservation request model to be added</param>
    /// <returns>ResponseReservationDto</returns>
    Task<ResponseReservationDto> CreateReservationAsync(RequestReservationDto reservationDTO);

    /// <summary>
    /// Update reservation
    /// </summary>
    /// <param name="id">Reservation id</param>
    /// <param name="reservationDTO">Reservation request model to be updated</param>
    /// <returns>ResponseReservationDto</returns>
    Task<ResponseReservationDto> UpdateReservationAsync(int id, RequestReservationDto reservationDTO);

    /// <summary>
    /// Get list of times with schedules availabilities base on branch and date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="date">Date filter</param>
    /// <returns>ICollection of TimeOnly</returns>
    Task<ICollection<TimeOnly>> ScheduleAvailabilityBranchAsync(byte branchId, DateOnly date);
}