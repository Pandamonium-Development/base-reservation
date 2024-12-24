using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
namespace BaseReservation.Application.Services.Implementations;

public class ServiceTypeService(IRepositoryTypeService repository, IMapper mapper) : IServiceTypeService
{
    /// <inheritdoc />
    public async Task<ResponseTypeServiceDto> FindByIdAsync(byte id)
    {
        var typeService = await repository.FindByIdAsync(id);
        if (typeService == null) throw new NotFoundException("Tipo de servicio no encontrado.");

        return mapper.Map<ResponseTypeServiceDto>(typeService);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseTypeServiceDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseTypeServiceDto>>(list);

        return collection;
    }
}