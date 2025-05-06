using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCanton(ICoreService<Canton> coreService, IMapper mapper) : IServiceCanton
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseCantonDto>> ListAllByProvinceAsync(long provinceId)
    {
        var spec = new BaseSpecification<Canton>(x => x.ProvinceId == provinceId);
        var list = await coreService.UnitOfWork.Repository<Canton>().ListAsync(spec);
        var collection = mapper.Map<ICollection<ResponseCantonDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseCantonDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Canton>().ExistsAsync(id)) throw new NotFoundException("Cantón no encontrado.");

        var spec = new BaseSpecification<Canton>(x => x.Id == id);
        var canton = await coreService.UnitOfWork.Repository<Canton>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseCantonDto>(canton);
    }
}