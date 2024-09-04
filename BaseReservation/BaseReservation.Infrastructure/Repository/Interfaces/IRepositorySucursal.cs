using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursal
{
    Task<Sucursal> CreateSucursalAsync(Sucursal sucursal);

    Task<Sucursal> UpdateSucursalAsync(Sucursal sucursal);

    Task<ICollection<Sucursal>> ListAllAsync();

    Task<ICollection<Sucursal>> ListAllByRoleAsync(string rol);

    Task<Sucursal?> FindByIdAsync(byte id);

    Task<bool> ExistsSucursalAsync(byte id);
}