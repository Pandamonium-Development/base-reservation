using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceBranchSchedule(IRepositoryBranchSchedule repository, IMapper mapper,
                                    IValidator<BranchSchedule> branchScheduleValidator) : IServiceBranchSchedule
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleAsync(byte branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
    {
        var schedules = await ValidateHorarios(branchId, branchSchedules);

        var result = await repository.CreateBranchSchedulesAsync(branchId, schedules);
        if (!result) throw new ListNotAddedException("Error al guardar horarios.");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleDto?> FindByIdAsync(short id)
    {
        var branchSchedule = await repository.FindByIdAsync(id);
        if (branchSchedule == null) throw new NotFoundException("Horario en sucursal no encontrado.");

        return mapper.Map<ResponseBranchScheduleDto>(branchSchedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleDto>> ListAllByBranchAsync(byte branchId)
    {
        var list = await repository.ListAllByBranchAsync(branchId);

        var collection = mapper.Map<ICollection<ResponseBranchScheduleDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate schedules
    /// </summary>
    /// <param name="branchId">Branch id to recevice schedules that need to be validated</param>
    /// <param name="branchSchedules">List of Branch's schedules request that need validation</param>
    /// <returns>IEnumerable of BranchSchedule</returns>
    private async Task<IEnumerable<BranchSchedule>> ValidateHorarios(byte branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
    {
        var existingSchedules = mapper.Map<List<BranchSchedule>>(branchSchedules);
        foreach (var item in existingSchedules)
        {
            item.BranchId = branchId;
            await branchScheduleValidator.ValidateAndThrowAsync(item);
        }
        return existingSchedules;
    }
}