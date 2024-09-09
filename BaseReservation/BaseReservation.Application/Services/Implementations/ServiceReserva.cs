using System.Globalization;
using BaseReservation.Application.Comunes;
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

public class ServiceReserva(IRepositoryReserva repository, IRepositorySucursalHorarioBloqueo repositorySucursalHorarioBloqueo,
                            IRepositorySucursalFeriado repositorySucursalFeriado, IMapper mapper,
                            IValidator<Reserva> reservaValidator, IRepositorySucursalHorario repositorySucursalHorario) : IServiceReserva
{
    const string formatoFecha = "yyyy-MM-dd";

    /// <inheritdoc />
    public async Task<ResponseReservaDto> CreateReservaAsync(RequestReservaDto reservaDTO)
    {
        var reserva = await ValidateReservaAsync(reservaDTO);

        var result = await repository.CreateReservaAsync(reserva);
        if (result == null) throw new NotFoundException("Reserva no se ha creado.");

        return mapper.Map<ResponseReservaDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseReservaDto> UpdateReservaAsync(int id, RequestReservaDto reservaDTO)
    {
        if (!await repository.ExistsReservaAsync(id)) throw new NotFoundException("Reserva no encontrada.");

        var reserva = await ValidateReservaAsync(reservaDTO);
        reserva.Id = id;
        var result = await repository.UpdateReservaAsync(reserva);

        return mapper.Map<ResponseReservaDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseReservaDto> FindByIdAsync(int id)
    {
        var reserva = await repository.FindByIdAsync(id);
        if (reserva == null) throw new NotFoundException("Reserva no encontrada.");

        return mapper.Map<ResponseReservaDto>(reserva);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseReservaDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseAgendaCalendarioReservaDto>> ListAllBySucursalAsync(byte idSucursal, DateOnly? startDate, DateOnly? endDate)
    {
        var list = startDate == null || endDate == null ? await repository.ListAllBySucursalAsync(idSucursal) : await repository.ListAllBySucursalAsync(idSucursal, startDate.Value, endDate.Value);

        var agendaCalendario = (from a in list
                                select new ResponseAgendaCalendarioReservaDto
                                {
                                    Title = $"{a.Id}-{a.NombreCliente}",
                                    Start = new DateTime(a.Fecha.Year, a.Fecha.Month, a.Fecha.Day, a.Hora.Hour, a.Hora.Minute, a.Hora.Second, DateTimeKind.Unspecified),
                                    End = new DateTime(a.Fecha.Year, a.Fecha.Month, a.Fecha.Day, a.Hora.Hour + 1, a.Hora.Minute, a.Hora.Second, DateTimeKind.Unspecified)
                                }).ToList();

        if (startDate != null && endDate != null)
        {
            var agendaBloqueos = await GetScheduleBlocksAsync(idSucursal, startDate.Value, endDate.Value);
            var feriados = await GetScheduleHolidaysAsync(idSucursal, startDate.Value, endDate.Value);

            if (feriados.Any()) agendaBloqueos = agendaBloqueos.Except(agendaBloqueos.Where(m => feriados.Exists(z => z.Start.ToString(formatoFecha) == m.Start.ToString(formatoFecha))).ToList()).ToList();

            agendaCalendario.AddRange(agendaBloqueos);
            agendaCalendario.AddRange(feriados);
        }

        return agendaCalendario;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservaDto>> ListAllBySucursalDiaAsync(byte idSucursal, DateOnly date)
    {
        var list = await repository.ListAllBySucursalAsync(idSucursal, date);
        var collection = mapper.Map<ICollection<ResponseReservaDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<TimeOnly>> ScheduleAvailabilityBySucursalAsync(byte idSucursal, DateOnly date)
    {
        var nombreDiaSemana = DateHourManipulation.GetDayWeekCultureCostaRica(date);
        App.DiaSemana diaSemana = (App.DiaSemana)Enum.Parse(typeof(App.DiaSemana), nombreDiaSemana);

        var horarioSucursal = await repositorySucursalHorario.FindByDiaSemanaAsync(idSucursal, mapper.Map<Infra.DiaSemana>(diaSemana));
        if (horarioSucursal == null) throw new NotFoundException("No se encontro horario en la sucursal.");

        var rangoHorario = DateHourManipulation.GetHoursAsync(horarioSucursal.IdHorarioNavigation.HoraInicio, horarioSucursal.IdHorarioNavigation.HoraFin.AddHours(-1));

        foreach (var item in horarioSucursal.SucursalHorarioBloqueos)
        {
            var rangoHorarioBloqueo = DateHourManipulation.GetHoursAsync(item.HoraInicio, item.HoraFin.AddHours(-1));
            rangoHorario = rangoHorario.Except(rangoHorarioBloqueo).ToList();
        }

        var reservas = await ListAllBySucursalDiaAsync(idSucursal, date);

        rangoHorario = rangoHorario.Except(reservas.Select(a => a.Hora)).ToList();

        return rangoHorario;
    }

    /// <summary>
    /// Validate reservation
    /// </summary>
    /// <param name="reservaDTO">Reservation request model to be added/updated</param>
    /// <returns>Reserva</returns>
    private async Task<Reserva> ValidateReservaAsync(RequestReservaDto reservaDTO)
    {
        var reserva = mapper.Map<Reserva>(reservaDTO);
        await reservaValidator.ValidateAndThrowAsync(reserva);
        return reserva;
    }

    /// <summary>
    /// Get list of schedule blocks in agenda mode by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseAgendaCalendarioReservaDto</returns>
    private async Task<List<ResponseAgendaCalendarioReservaDto>> GetScheduleBlocksAsync(byte idSucursal, DateOnly startDate, DateOnly endDate)
    {
        var bloqueos = await repositorySucursalHorarioBloqueo.ListAllBySucursalAsync(idSucursal);
        var diferenciaDias = DateHourManipulation.GetDaysAsync(startDate, endDate);
        var agendaBloqueos = from a in bloqueos
                             from b in diferenciaDias
                             where b.ToString("dddd", new CultureInfo("es-CR")).Capitalize().Replace("é", "e").Replace("á", "a") == Enum.GetName(typeof(DiaSemana), a.IdSucursalHorarioNavigation.IdHorarioNavigation.Dia)!
                             select new ResponseAgendaCalendarioReservaDto
                             {
                                 Title = "",
                                 Start = new DateTime(b.Year, b.Month, b.Day, a.HoraInicio.Hour, a.HoraInicio.Minute, a.HoraInicio.Second, DateTimeKind.Unspecified),
                                 End = new DateTime(b.Year, b.Month, b.Day, a.HoraFin.Hour, a.HoraFin.Minute, a.HoraFin.Second, DateTimeKind.Unspecified),
                                 Display = "background",
                                 ClassNames = "bg-danger"
                             };
        return agendaBloqueos.ToList();
    }

    /// <summary>
    /// Get list of schedule holidays in agenda mode by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="startDate">Start date</param>
    /// <param name="endDate">End date</param>
    /// <returns>List of ResponseAgendaCalendarioReservaDto</returns>
    private async Task<List<ResponseAgendaCalendarioReservaDto>> GetScheduleHolidaysAsync(byte idSucursal, DateOnly startDate, DateOnly endDate)
    {
        var feriados = await repositorySucursalFeriado.ListAllBySucursalAsync(idSucursal, startDate, endDate);
        var agendaFeriados = from a in feriados
                             select new ResponseAgendaCalendarioReservaDto
                             {
                                 Title = $"Feriado: {a.IdFeriadoNavigation.Nombre}",
                                 Start = DateTime.ParseExact(a.Fecha.ToString(formatoFecha), formatoFecha, CultureInfo.InvariantCulture),
                                 Display = "background",
                                 ClassNames = "bg-warning",
                                 AllDay = true,
                             };

        return agendaFeriados.ToList();
    }
}