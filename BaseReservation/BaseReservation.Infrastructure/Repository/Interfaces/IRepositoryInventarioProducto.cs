using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventarioProducto
{
    Task<bool> ExistsInventarioProductoAsync(long id);

    Task<InventarioProducto?> FindByIdAsync(long id);

    Task<ICollection<InventarioProducto>> ListAllByInventarioAsync(short idInventario);

    Task<ICollection<InventarioProducto>> ListAllByProductoAsync(short idProducto);

    Task<InventarioProducto> CreateProductoInventarioAsync(InventarioProducto inventarioProducto);

    Task<bool> CreateProductoInventarioAsync(IEnumerable<InventarioProducto> inventarioProducto);

    Task<InventarioProducto> UpdateProductoInventarioAsync(InventarioProducto inventarioProducto);
}