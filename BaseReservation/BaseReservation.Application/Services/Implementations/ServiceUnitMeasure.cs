using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUnitMeasure(IRepositoryUnitMeasure repository, IMapper mapper) : IServiceUnitMeasure
{
    /// <inheritdoc />
    public async Task<ResponseUnitMeasureDto> FindByIdAsync(byte id)
    {
        var unidad = await repository.FindByIdAsync(id);
        if (unidad == null) throw new NotFoundException("Unidad de medida no encontrada.");

        return mapper.Map<ResponseUnitMeasureDto>(unidad);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUnitMeasureDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseUnitMeasureDto>>(list);

        return collection;
    }
}