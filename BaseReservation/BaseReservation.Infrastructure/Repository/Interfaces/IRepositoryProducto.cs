using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProducto
{
    /// <summary>
    /// Creates a new Producto entity in the database.
    /// </summary>
    /// <param name="producto">The Producto entity to be created.</param>
    /// <returns>Producto</returns>
    Task<Producto> CreateProductoAsync(Producto producto);

    /// <summary>
    /// Updates an existing Producto entity in the database.
    /// </summary>
    /// <param name="producto">The Producto entity with update information.</param>
    /// <returns>Producto</returns>
    Task<Producto> UpdateProductoAsync(Producto producto);

    /// <summary>
    /// Retrieves all Producto entities, optionally excluding those present in a specific Inventario. }
    /// Default is false.
    /// </summary>
    /// <param name="excludeProductosInventario">Whether to exclude products that are in a specific Inventario.  </param>
    /// <param name="idInventario"></param>
    /// <returns>ICollection of Producto</returns>
    Task<ICollection<Producto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0);

    /// <summary>
    /// Retrieves a Producto entity by its ID, including related UnidadMedida and Categoria entities.
    /// </summary>
    /// <param name="id">The unique identifier of the Producto.</param>
    /// <returns>Producto if founded, otherwise null</returns>
    Task<Producto?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if a Producto with the specified ID exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the Producto.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsProductoAsync(short id);
}