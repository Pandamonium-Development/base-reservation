using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceHorario(IRepositoryHorario repository, IMapper mapper,
                            IValidator<Horario> horarioValidator) : IServiceHorario
{
    /// <inheritdoc />
    public async Task<ResponseHorarioDto> CreateHorarioAsync(RequestHorarioDto horarioDto)
    {
        var schedule = await ValidarHorario(horarioDto);

        var result = await repository.CreateHorarioAsync(schedule);
        if (result == null) throw new NotFoundException("Horario no se ha creado.");

        return mapper.Map<ResponseHorarioDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseHorarioDto> UpdateHorarioAsync(short id, RequestHorarioDto horarioDto)
    {
        if (!await repository.ExistsHorarioAsync(id)) throw new NotFoundException("Horario no encontrado.");

        var schedule = await ValidarHorario(horarioDto);
        schedule.Id = id;
        var result = await repository.UpdateHorarioAsync(schedule);

        return mapper.Map<ResponseHorarioDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseHorarioDto> FindByIdAsync(short id)
    {
        var schedule = await repository.FindByIdAsync(id);
        if (schedule == null) throw new NotFoundException("Horario no encontrada.");

        return mapper.Map<ResponseHorarioDto>(schedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseHorarioDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseHorarioDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteHorarioAsync(short id)
    {
        var schedule = await repository.FindByIdAsync(id);
        if (schedule == null) throw new NotFoundException("Horario no encontrada.");

        if (schedule.SucursalHorarios.Count > 0) throw new BaseReservationException("Horario no encontrada.");

        var result = await repository.DeleteHorarioAsync(id);
        return result;
    }

    /// <summary>
    /// Validates the `RequestHorarioDto` object by mapping it to a `Horario` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="horarioDTO">The `RequestHorarioDto` object containing the schedule data to validate.</param>
    /// <returns>Returns the validated `Horario` object.</returns>
    /// <exception cref="ValidationException">Thrown if the schedule data does not pass validation.</exception>
    private async Task<Horario> ValidarHorario(RequestHorarioDto horarioDTO)
    {
        var horario = mapper.Map<Horario>(horarioDTO);
        await horarioValidator.ValidateAndThrowAsync(horario);
        return horario;
    }
}