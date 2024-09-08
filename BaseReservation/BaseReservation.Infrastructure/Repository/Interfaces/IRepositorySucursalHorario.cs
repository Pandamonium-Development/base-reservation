using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalHorario
{
    /// <summary>
    /// Get list of all branch schedules by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of SucursalHorario</returns>
    Task<ICollection<SucursalHorario>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Get branch schedule by id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>SucursalHorario if found, otherwise null</returns>
    Task<SucursalHorario?> FindByIdAsync(short id);

    /// <summary>
    /// Get branch schedule by specific day of week
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="dia">Day of week</param>
    /// <returns>SucursalHorario if found, otherwise null</returns>
    Task<SucursalHorario?> FindByDiaSemanaAsync(byte idSucursal, DiaSemana dia);

    /// <summary>
    /// Create multiple branch schedule
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="sucursalHorarios">List of branch schedules</param>
    /// <returns>True if all were added, if not, false</returns>
    Task<bool> CreateSucursalHorariosAsync(byte idSucursal, IEnumerable<SucursalHorario> sucursalHorarios);
}