using System.Globalization;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using BaseReservation.Utils;
using AutoMapper;
using FluentValidation;
using App = BaseReservation.Application.ResponseDTOs.Enums;
using Infra = BaseReservation.Infrastructure.Enums;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceReservation(IRepositoryReservation repository, IRepositoryBranchScheduleBlock repositoryBranchScheduleBlock,
                            IRepositoryBranchHoliday repositoryBranchHoliday, IMapper mapper,
                            IValidator<Reservation> reservationValidator, IRepositoryBranchSchedule repositoryBranchSchedule) : IServiceReservation
{
    const string dateFormat = "yyyy-MM-dd";

    /// <inheritdoc />
    public async Task<ResponseReservationDto> CreateReservationAsync(RequestReservationDto reservationDTO)
    {
        var reservation = await ValidateReservationAsync(reservationDTO);

        var result = await repository.CreateReservationAsync(reservation);
        if (result == null) throw new NotFoundException("Reserva no se ha creado.");

        return mapper.Map<ResponseReservationDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseReservationDto> UpdateReservationAsync(int id, RequestReservationDto reservationDTO)
    {
        if (!await repository.ExistsReservationAsync(id)) throw new NotFoundException("Reserva no encontrada.");

        var reservation = await ValidateReservationAsync(reservationDTO);
        reservation.Id = id;
        var result = await repository.UpdateReservationAsync(reservation);

        return mapper.Map<ResponseReservationDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseReservationDto> FindByIdAsync(int id)
    {
        var reservation = await repository.FindByIdAsync(id);
        if (reservation == null) throw new NotFoundException("Reserva no encontrada.");

        return mapper.Map<ResponseReservationDto>(reservation);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseReservationDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationCalendarAgendaDto>> ListAllByBranchAsync(byte branchId, DateOnly? startDate, DateOnly? endDate)
    {
        var list = startDate == null || endDate == null ? await repository.ListAllByBranchAsync(branchId) : await repository.ListAllByBranchAsync(branchId, startDate.Value, endDate.Value);

        var calendarAgenda = (from a in list
                              select new ResponseReservationCalendarAgendaDto
                              {
                                  Title = $"{a.Id}-{a.CustomerName}",
                                  Start = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Hour.Hour, a.Hour.Minute, a.Hour.Second, DateTimeKind.Unspecified),
                                  End = new DateTime(a.Date.Year, a.Date.Month, a.Date.Day, a.Hour.Hour + 1, a.Hour.Minute, a.Hour.Second, DateTimeKind.Unspecified)
                              }).ToList();

        if (startDate != null && endDate != null)
        {
            var blocksAgenda = await GetScheduleBlocksAsync(branchId, startDate.Value, endDate.Value);
            var feriados = await GetScheduleHolidaysAsync(branchId, startDate.Value, endDate.Value);

            if (feriados.Any()) blocksAgenda = blocksAgenda.Except(blocksAgenda.Where(m => feriados.Exists(z => z.Start.ToString(dateFormat) == m.Start.ToString(dateFormat))).ToList()).ToList();

            calendarAgenda.AddRange(blocksAgenda);
            calendarAgenda.AddRange(feriados);
        }

        return calendarAgenda;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDto>> ListAllByBranchAsync(byte branchId, DateOnly date)
    {
        var list = await repository.ListAllByBranchAsync(branchId, date);
        var collection = mapper.Map<ICollection<ResponseReservationDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<TimeOnly>> ScheduleAvailabilityBranchAsync(byte branchId, DateOnly date)
    {
        var weekDayName = DateHourManipulation.GetDayWeekCultureCostaRica(date);
        WeekDay diaSemana = (WeekDay)Enum.Parse(typeof(WeekDay), weekDayName);

        var branchSchedule = await repositoryBranchSchedule.FindByWeekDayAsync(branchId, mapper.Map<Infra.WeekDay>(diaSemana));
        if (branchSchedule == null) throw new NotFoundException("No se encontro horario en la sucursal.");

        var scheduleRange = DateHourManipulation.GetHoursAsync(branchSchedule.ScheduleIdNavigation.StartHour, branchSchedule.ScheduleIdNavigation.EndHour.AddHours(-1));

        foreach (var item in branchSchedule.BranchScheduleBlocks)
        {
            var scheduleRangeBlocks = DateHourManipulation.GetHoursAsync(item.StartHour, item.EndHour.AddHours(-1));
            scheduleRange = scheduleRange.Except(scheduleRangeBlocks).ToList();
        }

        var reservations = await ListAllByBranchAsync(branchId, date);

        scheduleRange = scheduleRange.Except(reservations.Select(a => a.Hour)).ToList();

        return scheduleRange;
    }

    /// <summary>
    /// Validate reservation
    /// </summary>
    /// <param name="reservationDTO">Reservation request model to be added/updated</param>
    /// <returns>Reservation</returns>
    private async Task<Reservation> ValidateReservationAsync(RequestReservationDto reservationDTO)
    {
        var reservation = mapper.Map<Reservation>(reservationDTO);
        await reservationValidator.ValidateAndThrowAsync(reservation);
        return reservation;
    }

    /// <summary>
    /// Get list of schedule blocks in agenda mode by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseReservationCalendarAgendaDto</returns>
    private async Task<List<ResponseReservationCalendarAgendaDto>> GetScheduleBlocksAsync(byte branchId, DateOnly startDate, DateOnly endDate)
    {
        var blocks = await repositoryBranchScheduleBlock.ListAllByBranchAsync(branchId);
        var daysDiference = DateHourManipulation.GetDaysAsync(startDate, endDate);
        var blocksAgenda = from a in blocks
                           from b in daysDiference
                           where b.ToString("dddd", new CultureInfo("es-CR")).Capitalize().Replace("é", "e").Replace("á", "a") == Enum.GetName(typeof(WeekDay), a.BranchScheduleIdNavigation.ScheduleIdNavigation.Day)!
                           select new ResponseReservationCalendarAgendaDto
                           {
                               Title = "",
                               Start = new DateTime(b.Year, b.Month, b.Day, a.StartHour.Hour, a.StartHour.Minute, a.StartHour.Second, DateTimeKind.Unspecified),
                               End = new DateTime(b.Year, b.Month, b.Day, a.EndHour.Hour, a.EndHour.Minute, a.EndHour.Second, DateTimeKind.Unspecified),
                               Display = "background",
                               ClassNames = "bg-danger"
                           };
        return blocksAgenda.ToList();
    }

    /// <summary>
    /// Get list of schedule holidays in agenda mode by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseReservationCalendarAgendaDto</returns>
    private async Task<List<ResponseReservationCalendarAgendaDto>> GetScheduleHolidaysAsync(byte branchId, DateOnly startDate, DateOnly endDate)
    {
        var holidays = await repositoryBranchHoliday.ListAllByBranchAsync(branchId, startDate, endDate);
        var holidaysAgenda = from a in holidays
                             select new ResponseReservationCalendarAgendaDto
                             {
                                 Title = $"Feriado: {a.HolidayIdNavigation.Name}",
                                 Start = DateTime.ParseExact(a.Date.ToString(dateFormat), dateFormat, CultureInfo.InvariantCulture),
                                 Display = "background",
                                 ClassNames = "bg-warning",
                                 AllDay = true,
                             };

        return holidaysAgenda.ToList();
    }

    public async Task<bool> ExistsReservationAsync(int id) => await repository.ExistsReservationAsync(id);
}