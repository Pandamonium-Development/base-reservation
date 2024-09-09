using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceCategoria
{
    /// <summary>
    /// Finds a category by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>ResponseCategoriaDto</returns>
    /// <exception cref="NotFoundException">Thrown when no category is found with the specified ID.</exception>
    Task<ICollection<ResponseCategoriaDto>> ListAllAsync();

    /// <summary>
    /// Retrieves a list of all categories
    /// </summary>
    /// <returns>ICollection of ResponseCategoriaDto</returns>
    Task<ResponseCategoriaDto> FindByIdAsync(byte id);
}
