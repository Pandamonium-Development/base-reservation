using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalHorarioBloqueo
{
    /// <summary>
    /// Create a branch schedule block
    /// </summary>
    /// <param name="sucursalHorarioBloqueo">Branch schedule block model to be added</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo);

    /// <summary>
    /// Create multiple schedule branch blocks
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <param name="sucursalHorarioBloqueos">List of schedule branch blocks to be added</param>
    /// <returns>True if all items were saved, if not, false</returns>
    Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<SucursalHorarioBloqueo> sucursalHorarioBloqueos);

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="sucursalHorarioBloqueo">Branch schedule block model to be added</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo);

    /// <summary>
    /// Get list of all branch schedule blocks by branch schedule
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id</param>
    /// <returns>ICollection of SucursalHorarioBloqueo</returns>
    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalHorarioAsync(short idSucursalHorario);

    /// <summary>
    /// Get list of all branch schedule blocks by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of SucursalHorarioBloqueo</returns>
    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalAsync(byte idSucursal);

    /// <summary>
    /// Get branch schedule block with specific id
    /// </summary>
    /// <param name="id">Branch schedule block id</param>
    /// <returns>SucursalHorarioBloqueo</returns>
    Task<SucursalHorarioBloqueo?> FindByIdAsync(long id);

    /// <summary>
    /// Validate if exists branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block Id</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsSucursalHorarioBloqueoAsync(long id);
}