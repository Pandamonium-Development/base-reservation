using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProduct
{
    /// <summary>
    /// Creates a new Product entity in the database.
    /// </summary>
    /// <param name="product">The Product entity to be created.</param>
    /// <returns>Product</returns>
    Task<Product> CreateProductAsync(Product product);

    /// <summary>
    /// Updates an existing Product entity in the database.
    /// </summary>
    /// <param name="product">The Product entity with update information.</param>
    /// <returns>Product</returns>
    Task<Product> UpdateProductAsync(Product product);

    /// <summary>
    /// Retrieves all Product entities, optionally excluding those present in a specific Inventario. }
    /// Default is false.
    /// </summary>
    /// <param name="excludeProductsInventory">Whether to exclude products that are in a specific Inventario.  </param>
    /// <param name="inventoryId"></param>
    /// <returns>ICollection of Product</returns>
    Task<ICollection<Product>> ListAllAsync(bool excludeProductsInventory = false, short inventoryId = 0);

    /// <summary>
    /// Retrieves a Product entity by its ID, including related UnidadMedida and Categoria entities.
    /// </summary>
    /// <param name="id">The unique identifier of the Product.</param>
    /// <returns>Product if founded, otherwise null</returns>
    Task<Product?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if a Product with the specified ID exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the Product.</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsProductAsync(short id);
}