using AutoMapper;
using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCanton(IRepositoryCanton repository, IMapper mapper) : IServiceCanton
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseCantonDto>> ListAllByProvinceAsync(byte provinceId)
    {
        var list = await repository.ListAllByProvinceAsync(provinceId);
        var collection = mapper.Map<ICollection<ResponseCantonDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseCantonDto> FindByIdAsync(byte id)
    {
        var canton = await repository.FindByIdAsync(id);
        if (canton == null) throw new NotFoundException("Cantón no se ha encontrado.");

        return mapper.Map<ResponseCantonDto>(canton);
    }
}