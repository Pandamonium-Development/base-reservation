using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUsuarioSucursal
{
    Task<bool> AssignUsersAsync(byte idSucursal, IEnumerable<UsuarioSucursal> usuariosSucursal);

    Task<IEnumerable<UsuarioSucursal>> ListAllBySucursalAsync(byte idSucursal);
}