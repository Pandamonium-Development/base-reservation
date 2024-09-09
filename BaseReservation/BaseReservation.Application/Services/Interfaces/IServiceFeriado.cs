using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceFeriado
{
    /// <summary>
    ///  Creates a new ResponseFeriadoDto
    /// </summary>
    /// <param name="feriadoDTO">The data transfer object containing the information of the Factura to create</param>
    /// <returns>RequestFeriadoDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseFeriadoDto> CreateFeriadoAsync(RequestFeriadoDto feriadoDto);

    /// <summary>
    /// Deletes a holiday  by its identifier if it exists in the repository.
    /// </summary>
    /// <param name="id">The identifier of the holiday to delete.</param>
    /// <returns>>Returns `true` if the holiday was successfully deleted, otherwise `false`</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<bool> DeleteFeriadoAsync(byte id);

    /// <summary>
    /// Finds a feriado by its unique ID.
    /// </summary>
    /// <param name="id">The identifier of the holiday to retrieve.</param>
    /// <returns>ResponseFeriadoDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseFeriadoDto> FindByIdAsync(byte id);

    /// <summary>
    /// ICollection of ResponseFeriadoDto
    /// </summary>
    /// <returns>ResponseFeriadoDto</returns>
    Task<ICollection<ResponseFeriadoDto>> ListAllAsync();

    /// <summary>
    /// Updates an existing holiday identified by its ID with the provided data.
    /// </summary>
    /// <param name="id">The identifier of the holiday to update.</param>
    /// <param name="feriadoDTO">The data transfer object containing the updated holiday information.</param>
    /// <returns>RequestFeriadoDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseFeriadoDto> UpdateFeriadoAsync(byte id, RequestFeriadoDto feriadoDto);
}