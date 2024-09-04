using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProducto
{
    Task<Producto> CreateProductoAsync(Producto producto);

    Task<Producto> UpdateProductoAsync(Producto producto);

    Task<ICollection<Producto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0);

    Task<Producto?> FindByIdAsync(short id);

    Task<bool> ExistsProductoAsync(short id);
}
