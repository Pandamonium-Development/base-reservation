using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUsuario
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <param name="rol">Role name can be specified to filter</param>
    /// <returns>ICollection of ResponseUsuarioDto</returns>
    Task<ICollection<ResponseUsuarioDto>> ListAllAsync(string? rol = null);

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUsuarioDto</returns>
    Task<ResponseUsuarioDto> FindByIdAsync(short id);

    /// <summary>
    /// Verify if user can be assigned to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="idSucursalAsignacion">Branch id</param>
    /// <returns>True if is free, if not, false</returns>
    Task<bool> FreeAssignmentSucursalAsync(short id, byte idSucursalAsignacion);
}