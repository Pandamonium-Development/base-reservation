using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDistrito
{
    /// <summary>
    /// Retrieves a list of all ResponseDistritoDto.
    /// </summary>
    /// <param name="idCanton">The identifier of the ResponseDistritoDto</param>
    /// <returns>ICollection of ResponseDistritoDto</returns>
    Task<ICollection<ResponseDistritoDto>> ListAllByCantonAsync(byte idCanton);

    /// <summary>
    ///  Finds a ResponseDistritoDto by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the ResponseDistritoDto to retrieve.</param>
    /// <returns>ResponseDistritoDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseDistritoDto> FindByIdAsync(byte id);
}
