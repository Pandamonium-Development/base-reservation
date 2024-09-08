using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryInventarioProducto
{
    /// <summary>
    /// Checks if an InventarioProducto with the specified identifier exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the InventarioProducto.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsInventarioProductoAsync(long id);

    /// <summary>
    /// Finds an InventarioProdcuto by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the InventarioProducto</param>
    /// <returns>InventarioProducto if founded, otherwise null</returns>
    Task<InventarioProducto?> FindByIdAsync(long id);

    /// <summary>
    /// Lists all InventarioProducto entities associated with a specific Inventario.
    /// </summary>
    /// <param name="idInventario">tThe unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventarioProducto</returns>
    Task<ICollection<InventarioProducto>> ListAllByInventarioAsync(short idInventario);

    /// <summary>
    /// Lists all InventarioProducto entities associated with a specific Producto.
    /// </summary>
    /// <param name="idProducto">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventarioProducto </returns>
    Task<ICollection<InventarioProducto>> ListAllByProductoAsync(short idProducto);

    /// <summary>
    /// Creates a new InventarioProducto
    /// </summary>
    /// <param name="inventarioProducto">The InventarioProdcuto entity to be added.</param>
    /// <returns>InventarioProducto</returns>
    Task<InventarioProducto> CreateProductoInventarioAsync(InventarioProducto inventarioProducto);

    /// <summary>
    /// Creates multiple InventarioProducto entities.
    /// </summary>
    /// <param name="inventarioProducto">A collection of InventarioProducto entities to be added.</param>
    /// <returns>True if were added, if not, false</returns>
    Task<bool> CreateProductoInventarioAsync(IEnumerable<InventarioProducto> inventarioProducto);

    /// <summary>
    /// Updates an existing InventarioProducto entity.
    /// </summary>
    /// <param name="inventarioProducto">The InventarioProducto entity to update.</param>
    /// <returns>InventarioProducto</returns>
    Task<InventarioProducto> UpdateProductoInventarioAsync(InventarioProducto inventarioProducto);
}