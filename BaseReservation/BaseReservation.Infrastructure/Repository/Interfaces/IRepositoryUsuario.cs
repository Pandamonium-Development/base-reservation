using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUsuario
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <returns>ICollection of Usuario</returns>
    Task<ICollection<Usuario>> ListAllAsync();

    /// <summary>
    /// Get list of all users assigned to an specific role
    /// </summary>
    /// <param name="idRol">Role id</param>
    /// <returns>ICollection Usuario</returns>
    Task<ICollection<Usuario>> ListAllByRoleAsync(byte idRol);

    /// <summary>
    /// Validat if the user can be assigned to another branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="idSucursalAsignacion">Branch to be assigned</param>
    /// <returns>True if is available, if not, false</returns>
    Task<bool> IsAvailableAsync(short id, byte idSucursalAsignacion);

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    Task<Usuario?> FindByIdAsync(short id);

    /// <summary>
    /// Validate if the user already exists
    /// </summary>
    /// <param name="id">User id</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsUsuarioAsync(short id);

    /// <summary>
    /// Get user with specific email
    /// </summary>
    /// <param name="correoElectronico">User's email</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    Task<Usuario?> FindByEmailAsync(string correoElectronico);

    /// <summary>
    /// Get user that can be logged in into system
    /// </summary>
    /// <param name="correoElectronico">User's email</param>
    /// <param name="contrasenna">User's password encrypted</param>
    /// <returns>Usuario if founded, otherwise null</returns>
    Task<Usuario?> LoginAsync(string correoElectronico, string contrasenna);
}