using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventarioProducto
{
    Task<ResponseInventarioProductoDto> FindByIdAsync(long id);

    Task<ICollection<ResponseInventarioProductoDto>> ListAllByInventarioAsync(short idInventario);

    Task<ICollection<ResponseInventarioProductoDto>> ListAllByProductoAsync(short idProducto);

    Task<ResponseInventarioProductoDto> CreateProductoInventarioAsync(RequestInventarioProductoDto inventarioProductoDto);

    Task<bool> CreateProductoInventarioAsync(IEnumerable<RequestInventarioProductoDto> inventarioProductosDto);

    Task<ResponseInventarioProductoDto> UpdateProductoInventarioAsync(long idInventarioProducto, RequestInventarioProductoDto inventarioProductoDto);
}