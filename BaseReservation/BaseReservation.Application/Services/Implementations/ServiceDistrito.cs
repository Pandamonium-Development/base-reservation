using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceDistrito(IRepositoryDistrito repository, IMapper mapper) : IServiceDistrito
{
    /// <inheritdoc />
    public async Task<ResponseDistritoDto> FindByIdAsync(byte id)
    {
        var distrito = await repository.FindByIdAsync(id);
        if (distrito == null) throw new NotFoundException("Distrito no encontrado.");

        return mapper.Map<ResponseDistritoDto>(distrito);
    }
    /// <inheritdoc />
    public async Task<ICollection<ResponseDistritoDto>> ListAllByCantonAsync(byte idCanton)
    {
        var list = await repository.ListAllByCantonAsync(idCanton);
        var collection = mapper.Map<ICollection<ResponseDistritoDto>>(list);

        return collection;
    }
}
