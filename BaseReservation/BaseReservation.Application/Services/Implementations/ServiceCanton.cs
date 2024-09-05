using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCanton(IRepositoryCanton repository, IMapper mapper) : IServiceCanton
{
    /// <summary>
    /// Retrieves a list of all cantons by the given province ID.
    /// </summary>
    /// <param name="idProvincia">The ID of the province to filter cantons.</param>
    /// <returns>ICollection of ResponseCantonDto</returns>
    public async Task<ICollection<ResponseCantonDto>> ListAllByProvinciaAsync(byte idProvincia)
    {
        var list = await repository.ListAllByProvinciaAsync(idProvincia);
        var collection = mapper.Map<ICollection<ResponseCantonDto>>(list);

        return collection;
    }

    /// <summary>
    /// Finds a canton by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the canton to retrieve. </param>
    /// <returns>ResponseCantonDto</returns>
    /// <exception cref="NotFoundException">Thrown when no canton is found with the specified ID</exception>
    public async Task<ResponseCantonDto?> FindByIdAsync(byte id)
    {
        var canton = await repository.FindByIdAsync(id);
        if (canton == null) throw new NotFoundException("Cantón no se ha encontrado.");

        return mapper.Map<ResponseCantonDto>(canton);
    }
}