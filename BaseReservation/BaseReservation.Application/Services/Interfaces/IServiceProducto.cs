using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProducto
{
    /// <summary>
    /// Get list of all products
    /// </summary>
    /// <param name="excludeProductosInventario">Indicator to excludes products from specific inventory</param>
    /// <param name="idInventario">Inventory id to be excluded</param>
    /// <returns>ICollection of ResponseProductoDto</returns>
    Task<ICollection<ResponseProductoDto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0);

    /// <summary>
    /// Get producto with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProductoDto</returns>
    Task<ResponseProductoDto> FindByIdAsync(short id);

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="productoDTO">Product request model to be added</param>
    /// <returns>ResponseProductoDto</returns>
    Task<ResponseProductoDto> CreateProductoAsync(RequestProductoDto productoDTO);

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="id">Product if</param>
    /// <param name="productoDTO">Product request model to be updated</param>
    /// <returns>ResponseProductoDto</returns>
    Task<ResponseProductoDto> UpdateProductoAsync(short id, RequestProductoDto productoDTO);
}