using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceHoliday(IRepositoryHoliday repository, IMapper mapper,
                            IValidator<Holiday> holidayValidator) : IServiceHoliday
{
    /// <inheritdoc />
    public async Task<ResponseHolidayDto> CreateHolidayAsync(RequestHolidayDto holidayDto)
    {
        var holiday = await ValidarHoliday(holidayDto);

        var result = await repository.CreateHolidayAsync(holiday);
        if (result == null) throw new NotFoundException("Feriado no creado.");

        return mapper.Map<ResponseHolidayDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseHolidayDto> UpdateHolidayAsync(byte id, RequestHolidayDto holidayDto)
    {
        if (!await repository.ExistsHolidayAsync(id)) throw new NotFoundException("Feriado no encontrada.");

        var holiday = await ValidarHoliday(holidayDto);
        holiday.Id = id;
        var result = await repository.UpdateHolidayAsync(holiday);

        return mapper.Map<ResponseHolidayDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteHolidayAsync(byte id)
    {
        if (!await repository.ExistsHolidayAsync(id)) throw new NotFoundException("Feriado no encontrada.");
        return await repository.DeleteHolidayAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseHolidayDto> FindByIdAsync(byte id)
    {
        var holiday = await repository.FindByIdAsync(id);
        if (holiday == null) throw new NotFoundException("Feriado no encontrado.");

        return mapper.Map<ResponseHolidayDto>(holiday);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseHolidayDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseHolidayDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validates the `RequestHolidayDto` object by mapping it to a `Holiday` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="holidayDTO">The `RequestHolidayDto` object containing the holiday data to validate</param>
    /// <returns>RequestHolidayDto</returns>
    private async Task<Holiday> ValidarHoliday(RequestHolidayDto holidayDTO)
    {
        var holiday = mapper.Map<Holiday>(holidayDTO);
        await holidayValidator.ValidateAndThrowAsync(holiday);
        return holiday;
    }
}