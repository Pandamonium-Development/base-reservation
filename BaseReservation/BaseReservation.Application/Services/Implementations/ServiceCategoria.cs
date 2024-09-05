using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceCategoria(IRepositoryCategoria repository, IMapper mapper) : IServiceCategoria
{
    /// <summary>
    /// Finds a category by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>ResponseCategoriaDto</returns>
    /// <exception cref="NotFoundException">Thrown when no category is found with the specified ID.</exception>
    public async Task<ResponseCategoriaDto> FindByIdAsync(byte id)
    {
        var categoria = await repository.FindByIdAsync(id);
        if (categoria == null) throw new NotFoundException("Categoría no se ha encontrado.");

        return mapper.Map<ResponseCategoriaDto>(categoria);
    }

    /// <summary>
    /// Retrieves a list of all categories
    /// </summary>
    /// <returns>ICollection of ResponseCategoriaDto</returns>
    public async Task<ICollection<ResponseCategoriaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseCategoriaDto>>(list);

        return collection;
    }
}