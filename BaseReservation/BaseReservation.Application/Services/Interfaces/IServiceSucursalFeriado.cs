using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursalFeriado
{
    /// <summary>
    /// Get list of all holiday's branch by year
    /// </summary>
    /// <param name="idSucursal">Branch id to look for</param>
    /// <param name="anno">Year to look for/param>
    /// <returns>ICollection of ResponseSucursalFeriadoDto</returns>
    Task<ICollection<ResponseSucursalFeriadoDto>> ListAllBySucursalAsync(byte idSucursal, short? anno);

    /// <summary>
    /// Get Branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseSucursalFeriadoDto</returns>
    Task<ResponseSucursalFeriadoDto> FindByIdAsync(short id);

    /// <summary>
    /// Create branch's holidays
    /// </summary>
    /// <param name="idSucursal">Branch id to receive holidays</param>
    /// <param name="sucursalFeriados">List of branch holiday request mode to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<RequestSucursalFeriadoDto> sucursalFeriados);
}