using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUnidadMedida(IRepositoryUnidadMedida repository, IMapper mapper) : IServiceUnidadMedida
{
    /// <inheritdoc />
    public async Task<ResponseUnidadMedidaDto> FindByIdAsync(byte id)
    {
        var unidad = await repository.FindByIdAsync(id);
        if (unidad == null) throw new NotFoundException("Unidad de medida no encontrada.");

        return mapper.Map<ResponseUnidadMedidaDto>(unidad);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUnidadMedidaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseUnidadMedidaDto>>(list);

        return collection;
    }
}