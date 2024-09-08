using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventarioProducto
{
    /// <summary>
    /// Get inventory product with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    Task<ResponseInventarioProductoDto> FindByIdAsync(long id);

    /// <summary>
    /// Get list of all inventory product by inventory
    /// </summary>
    /// <param name="idInventario">Inventory id</param>
    /// <returns>ICollection of ResponseInventarioProductoDto</returns>
    Task<ICollection<ResponseInventarioProductoDto>> ListAllByInventarioAsync(short idInventario);

    /// <summary>
    /// Get list of all inventory product by product
    /// </summary>
    /// <param name="idProducto">Product id</param>
    /// <returns>ICollection of ResponseInventarioProductoDto</returns>
    Task<ICollection<ResponseInventarioProductoDto>> ListAllByProductoAsync(short idProducto);

    /// <summary>
    /// Create inventory product
    /// </summary>
    /// <param name="inventarioProductoDto">Inventary product request model to be added</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    Task<ResponseInventarioProductoDto> CreateProductoInventarioAsync(RequestInventarioProductoDto inventarioProductoDto);

    /// <summary>
    /// Create inventory products
    /// </summary>
    /// <param name="inventarioProductosDto">List of Inventary product request model to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateProductoInventarioAsync(IEnumerable<RequestInventarioProductoDto> inventarioProductosDto);

    /// <summary>
    /// Update inventory product
    /// </summary>
    /// <param name="idInventarioProducto">Inventory Product id</param>
    /// <param name="inventarioProductoDto">Inventary product request model to be updated</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    Task<ResponseInventarioProductoDto> UpdateProductoInventarioAsync(long idInventarioProducto, RequestInventarioProductoDto inventarioProductoDto);
}