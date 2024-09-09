
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalFeriado
{
    /// <summary>
    /// Get list of all branch holidays by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Get list of all branch holidays by branch, start date and end date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="fechaInicio">Start date to filter</param>
    /// <param name="fechaFin">End date to filter</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin);

    /// <summary>
    /// Get list of all branch holidays by branch and year
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="anno">Year to look for</param>
    /// <returns>ICollection of SucursalFeriado</returns>
    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, short anno);

    /// <summary>
    /// Get list of branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>SucursalFeriado if founded, otherwise null</returns>
    Task<SucursalFeriado?> FindByIdAsync(short id);

    /// <summary>
    /// Create branch holidays
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursalFeriados">List of branch holidays to be added</param>
    /// <returns>True if all were added, if not, false</returns>
    Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<SucursalFeriado> sucursalFeriados);
}