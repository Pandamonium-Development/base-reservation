using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCanton(IRepositoryCanton repository, IMapper mapper) : IServiceCanton
{
    /// <inheritdoc />
    public async Task<ICollection<ResponseCantonDto>> ListAllByProvinciaAsync(byte idProvincia)
    {
        var list = await repository.ListAllByProvinciaAsync(idProvincia);
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