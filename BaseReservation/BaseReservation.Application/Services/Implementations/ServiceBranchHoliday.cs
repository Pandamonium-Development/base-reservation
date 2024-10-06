using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceBranchHoliday(IRepositoryBranchHoliday repository, IMapper mapper,
                                    IValidator<BranchHoliday> branchHolidayValidator) : IServiceBranchHoliday
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchHolidaysAsync(byte branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays)
    {
        var holidays = await ValidateFeriados(branchId, branchHolidays);

        var result = await repository.CreateBranchHolidaysAsync(branchId, holidays);
        if (!result) throw new ListNotAddedException("Error al guardar feriados");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchHolidayDto> FindByIdAsync(short id)
    {
        var branchHoliday = await repository.FindByIdAsync(id);
        if (branchHoliday == null) throw new NotFoundException("Feriado en sucursal no encontrado.");

        return mapper.Map<ResponseBranchHolidayDto>(branchHoliday);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(byte branchId, short? year)
    {
        var list = year == null ? await repository.ListAllByBranchAsync(branchId) :
                               await repository.ListAllByBranchAsync(branchId, year.Value);
        var collection = mapper.Map<ICollection<ResponseBranchHolidayDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    private async Task<IEnumerable<BranchHoliday>> ValidateFeriados(byte branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays)
    {
        var holidays = mapper.Map<List<BranchHoliday>>(branchHolidays);
        foreach (var item in holidays)
        {
            item.BranchId = branchId;
            await branchHolidayValidator.ValidateAndThrowAsync(item);
        }
        return holidays;
    }
}