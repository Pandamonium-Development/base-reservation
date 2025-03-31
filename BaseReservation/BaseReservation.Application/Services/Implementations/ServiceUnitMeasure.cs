using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUnitMeasure(ICoreService<UnitMeasure> coreService, IMapper mapper) : IServiceUnitMeasure
{
    /// <inheritdoc />
    public async Task<ResponseUnitMeasureDto> FindByIdAsync(byte id)
    {
        if (!await coreService.UnitOfWork.Repository<UnitMeasure>().ExistsAsync(id)) throw new NotFoundException("Unidad de medida no encontrada.");

        var spec = new BaseSpecification<UnitMeasure>(x => x.Id == id);
        var unitMeasure = await coreService.UnitOfWork.Repository<UnitMeasure>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseUnitMeasureDto>(unitMeasure);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUnitMeasureDto>> ListAllAsync()
    {
        var unitMeasures = await coreService.UnitOfWork.Repository<UnitMeasure>().ListAllAsync();

        return mapper.Map<ICollection<ResponseUnitMeasureDto>>(unitMeasures);
    }
}