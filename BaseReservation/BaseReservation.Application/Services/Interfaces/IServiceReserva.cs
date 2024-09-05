﻿using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReserva
{
    Task<ICollection<ResponseReservaDto>> ListAllAsync();

    Task<ICollection<ResponseAgendaCalendarioReservaDto>> ListAllBySucursalAsync(byte idSucursal, DateOnly? startDate, DateOnly? endDate);

    Task<ResponseReservaDto> FindByIdAsync(int id);

    Task<ResponseReservaDto> CreateReservaAsync(RequestReservaDto reservaDTO);

    Task<ResponseReservaDto> UpdateReservaAsync(int id, RequestReservaDto reservaDTO);

    Task<ICollection<ResponseReservaDto>> ListAllBySucursalDiaAsync(byte idSucursal, DateOnly date);

    Task<ICollection<TimeOnly>> ScheduleAvailabilityBySucursalAsync(byte idSucursal, DateOnly date);
}