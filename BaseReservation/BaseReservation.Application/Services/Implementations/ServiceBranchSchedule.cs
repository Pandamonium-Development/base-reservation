using AutoMapper;
using FluentValidation;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Enums;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceBranchSchedule(ICoreService<BranchSchedule> coreService, IMapper mapper,
                                    IValidator<BranchSchedule> branchScheduleValidator) : IServiceBranchSchedule
{
    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleAsync(long branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
    {
        var schedules = await ValidateHorarios(branchId, branchSchedules);

        await coreService.UnitOfWork.Repository<BranchSchedule>().AddRangeAsync(schedules.ToList());

        int rowsAffected = await coreService.UnitOfWork.SaveChangesAsync();
        if (rowsAffected == 0) throw new ListNotAddedException("Error al guardar horarios.");

        return true;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleDto?> FindByIdAsync(long id)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.Id == id);
        var branchSchedule = await coreService.UnitOfWork.Repository<BranchSchedule>().FirstOrDefaultAsync(spec);
        if (branchSchedule == null) throw new NotFoundException("Horario en sucursal no encontrado.");

        return mapper.Map<ResponseBranchScheduleDto>(branchSchedule);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleDto>> ListAllByBranchAsync(long branchId)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.BranchId == branchId);
        var branchSchedules = await coreService.UnitOfWork.Repository<BranchSchedule>().ListAsync(spec, ["BranchScheduleBlocks"]);

        return mapper.Map<ICollection<ResponseBranchScheduleDto>>(branchSchedules);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleDto> FindByWeekDayAsync(long branchId, WeekDayApplication weekDay)
    {
        var spec = new BaseSpecification<BranchSchedule>(x => x.BranchId == branchId && x.ScheduleIdNavigation.Day == mapper.Map<WeekDay>(weekDay));
        var branchSchedule = await coreService.UnitOfWork.Repository<BranchSchedule>().FirstOrDefaultAsync(spec, ["ScheduleIdNavigation", "BranchScheduleBlocks"]);

        if (branchSchedule == null) throw new NotFoundException("No se encontro horario en la sucursal.");

        return mapper.Map<ResponseBranchScheduleDto>(branchSchedule);
    }

    /// <summary>
    /// Validate schedules
    /// </summary>
    /// <param name="branchId">Branch id to recevice schedules that need to be validated</param>
    /// <param name="branchSchedules">List of Branch's schedules request that need validation</param>
    /// <returns>IEnumerable of BranchSchedule</returns>
    private async Task<IEnumerable<BranchSchedule>> ValidateHorarios(long branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules)
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