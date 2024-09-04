using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUsuarioSucursal
{
    Task<bool> AssignUsuariosAsync(byte idSucursal, IEnumerable<UsuarioSucursal> usuariosSucursal);

    Task<ICollection<UsuarioSucursal>> ListAllBySucursalAsync(byte idSucursal);
}