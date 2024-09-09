using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceProvincia(IRepositoryProvincia repository, IMapper mapper) : IServiceProvincia
{
    /// <inheritdoc />
    public async Task<ResponseProvinciaDto> FindByIdAsync(byte id)
    {
        var provincia = await repository.FindByIdAsync(id);
        if (provincia == null) throw new NotFoundException("Provincia no encontrada.");

        return mapper.Map<ResponseProvinciaDto>(provincia);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProvinciaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseProvinciaDto>>(list);

        return collection;
    }
}