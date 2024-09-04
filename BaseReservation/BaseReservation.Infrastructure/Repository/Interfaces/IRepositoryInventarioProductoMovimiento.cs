
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventarioProductoMovimiento
{
    Task<ICollection<InventarioProductoMovimiento>> ListAllByInventarioAsync(short idInventario);

    Task<ICollection<InventarioProductoMovimiento>> ListAllByProductoAsync(short idProducto);

    Task<bool> CreateInventarioMovimientoProductoAsync(InventarioProductoMovimiento inventarioProductoMovimiento);
}