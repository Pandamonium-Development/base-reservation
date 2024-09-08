using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursalHorarioBloqueo
{
    /// <summary>
    /// Get list of all blocks by branch schedule
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <returns>ICollection of ResponseSucursalHorarioBloqueoDto</returns>
    Task<ICollection<ResponseSucursalHorarioBloqueoDto>> ListAllBySucursalHorarioAsync(short idSucursalHorario);

    /// <summary>
    /// Get Branch schedule block with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseSucursalHorarioBloqueoDto</returns>
    Task<ResponseSucursalHorarioBloqueoDto> FindByIdAsync(long id);

    /// <summary>
    /// Create branch schedule block
    /// </summary>
    /// <param name="bloqueoDTO">Branch schedule block request model to be added</param>
    /// <returns>ResponseSucursalHorarioBloqueoDto</returns>
    Task<ResponseSucursalHorarioBloqueoDto> CreateSucursalHorarioBloqueoAsync(RequestSucursalHorarioBloqueoDto bloqueoDTO);

    /// <summary>
    /// Create branch schedule's blocks
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id that receive blocks</param>
    /// <param name="bloqueos">List of Branch schedule's blocks will be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<RequestSucursalHorarioBloqueoDto> bloqueos);

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block id to identiy the record</param>
    /// <param name="bloqueoDTO">Branch schedule block request model to be updated</param>
    /// <returns>ResponseSucursalHorarioBloqueoDto</returns>
    Task<ResponseSucursalHorarioBloqueoDto> UpdateSucursalHorarioBloqueoAsync(long id, RequestSucursalHorarioBloqueoDto bloqueoDTO);
}