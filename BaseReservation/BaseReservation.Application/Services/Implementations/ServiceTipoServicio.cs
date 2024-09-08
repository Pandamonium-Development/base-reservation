using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
namespace BaseReservation.Application.Services.Implementations;

public class ServiceTipoServicio(IRepositoryTipoServicio repository, IMapper mapper) : IServiceTipoServicio
{
    /// <inheritdoc />
    public async Task<ResponseTipoServicioDto> FindByIdAsync(byte id)
    {
        var tipoServicio = await repository.FindByIdAsync(id);
        if (tipoServicio == null) throw new NotFoundException("Tipo de servicio no encontrado.");

        return mapper.Map<ResponseTipoServicioDto>(tipoServicio);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseTipoServicioDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseTipoServicioDto>>(list);

        return collection;
    }
}