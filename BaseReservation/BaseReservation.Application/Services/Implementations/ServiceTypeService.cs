using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;
namespace BaseReservation.Application.Services.Implementations;

public class ServiceTypeService(ICoreService<TypeService> coreService, IMapper mapper) : IServiceTypeService
{
    /// <inheritdoc />
    public async Task<ResponseTypeServiceDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<TypeService>().ExistsAsync(id)) throw new NotFoundException("Tipo de servicio no encontrado.");

        var spec = new BaseSpecification<TypeService>(x => x.Id == id);
        var typeService = await coreService.UnitOfWork.Repository<TypeService>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseTypeServiceDto>(typeService);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseTypeServiceDto>> ListAllAsync()
    {
        var typesService = await coreService.UnitOfWork.Repository<TypeService>().ListAllAsync();
        return mapper.Map<ICollection<ResponseTypeServiceDto>>(typesService);
    }
}