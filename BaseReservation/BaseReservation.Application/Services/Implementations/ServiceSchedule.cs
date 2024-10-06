using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSchedule(IRepositorySchedule repository, IMapper mapper,
                            IValidator<Schedule> scheduleValidator) : IServiceSchedule
{
    /// <inheritdoc />
    public async Task<ResponseScheduleDto> CreateScheduleAsync(RequestScheduleDto scheduleDto)
    {
        var schedule = await ValidateSchedule(scheduleDto);

        var result = await repository.CreateScheduleAsync(schedule);
        if (result == null) throw new NotFoundException("Horario no se ha creado.");

        return mapper.Map<ResponseScheduleDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseScheduleDto> UpdateScheduleAsync(short id, RequestScheduleDto scheduleDto)
    {
        if (!await repository.ExistsScheduleAsync(id)) throw new NotFoundException("Horario no encontrado.");

        var schedule = await ValidateSchedule(scheduleDto);
        schedule.Id = id;
        var result = await repository.UpdateScheduleAsync(schedule);

        return mapper.Map<ResponseScheduleDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseScheduleDto> FindByIdAsync(short id)
    {
        var schedule = await repository.FindByIdAsync(id);
        if (schedule == null) throw new NotFoundException("Horario no encontrada.");

        return mapper.Map<ResponseScheduleDto>(schedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseScheduleDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseScheduleDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteScheduleAsync(short id)
    {
        var schedule = await repository.FindByIdAsync(id);
        if (schedule == null) throw new NotFoundException("Horario no encontrada.");

        if (schedule.BranchSchedules.Count > 0) throw new BaseReservationException("Horario asignado en sucursales.");

        var result = await repository.DeleteScheduleAsync(id);
        return result;
    }

    /// <summary>
    /// Validates the `RequestScheduleDto` object by mapping it to a `Schedule` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="scheduleDTO">The `RequestScheduleDto` object containing the schedule data to validate.</param>
    /// <returns>Returns the validated `Schedule` object.</returns>
    /// <exception cref="ValidationException">Thrown if the schedule data does not pass validation.</exception>
    private async Task<Schedule> ValidateSchedule(RequestScheduleDto scheduleDTO)
    {
        var schedule = mapper.Map<Schedule>(scheduleDTO);
        await scheduleValidator.ValidateAndThrowAsync(schedule);
        return schedule;
    }
}