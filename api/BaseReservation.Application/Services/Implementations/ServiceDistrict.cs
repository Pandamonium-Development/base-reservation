using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceDistrict(ICoreService<District> coreService, IMapper mapper) : IServiceDistrict
{
    /// <inheritdoc />
    public async Task<ResponseDistrictDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<District>().ExistsAsync(id)) throw new NotFoundException("District no encontrado.");

        var spec = new BaseSpecification<District>(x => x.Id == id);
        var district = await coreService.UnitOfWork.Repository<District>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseDistrictDto>(district);
    }
    /// <inheritdoc />
    public async Task<ICollection<ResponseDistrictDto>> ListAllByCantonAsync(long cantonId)
    {
        var spec = new BaseSpecification<District>(x => x.CantonId == cantonId);
        var districts = await coreService.UnitOfWork.Repository<District>().ListAsync(spec);

        return mapper.Map<ICollection<ResponseDistrictDto>>(districts);
    }
}
