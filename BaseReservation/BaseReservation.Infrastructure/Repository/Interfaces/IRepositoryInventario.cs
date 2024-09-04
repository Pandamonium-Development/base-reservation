using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventario
{
    Task<Inventario> CreateInventarioAsync(Inventario inventario);

    Task<Inventario> UpdateInventarioAsync(Inventario inventario);

    Task<ICollection<Inventario>> ListAllAsync();

    Task<ICollection<Inventario>> ListAllBySucursalAsync(byte idSucursal);

    Task<Inventario?> FindByIdAsync(short id);

    Task<bool> ExistsInventarioAsync(short id);

    Task<bool> DeleteInventarioAsync(short id);
}