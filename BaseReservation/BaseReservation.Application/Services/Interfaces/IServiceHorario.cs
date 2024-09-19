using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceHorario
{
    /// <summary>
    ///  Creates a new schedule
    /// </summary>
    /// <param name="horarioDTO">Schedule request model to be added</param>
    /// <returns>ResponseHorarioDto</returns>
    Task<ResponseHorarioDto> CreateHorarioAsync(RequestHorarioDto horarioDto);

    /// <summary>
    /// Updates an existing schedule
    /// </summary>
    /// <param name="id">Schedule id to identity record</param>
    /// <param name="horarioDTO">Schedule request model to be updated</param>
    /// <returns>ResponseHorarioDto</returns>
    Task<ResponseHorarioDto> UpdateHorarioAsync(short id, RequestHorarioDto horarioDto);

    /// <summary>
    /// Get list of all ResponseHorarioDto.
    /// </summary>
    /// <returns>ICollection of ResponseHorarioDto</returns>
    Task<ICollection<ResponseHorarioDto>> ListAllAsync();

    /// <summary>
    /// Finds a schedule by its unique identifier
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseHorarioDto</returns>
    Task<ResponseHorarioDto> FindByIdAsync(short id);

    /// <summary>
    /// Delete existing schedule
    /// </summary>
    /// <param name="id">Schedule id to look for</param>
    /// <returns>bool</returns>
    Task<bool> DeleteHorarioAsync(short id);
}