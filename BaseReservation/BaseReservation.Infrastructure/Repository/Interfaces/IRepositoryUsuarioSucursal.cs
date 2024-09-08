using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUsuarioSucursal
{
    /// <summary>
    /// Assign users to a branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="usuariosSucursal">List of users to be assign</param>
    /// <returns>True if all users were added correctly, if not, false</returns>
    Task<bool> AssignUsuariosAsync(byte idSucursal, IEnumerable<UsuarioSucursal> usuariosSucursal);

    /// <summary>
    /// Get list of all user by a branch
    /// </summary>
    /// <param name="idSucursal">Branch id to filter</param>
    /// <returns>ICollection of UsuarioSucursal</returns>
    Task<ICollection<UsuarioSucursal>> ListAllBySucursalAsync(byte idSucursal);
}