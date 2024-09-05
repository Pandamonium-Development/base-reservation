using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProducto
{
    Task<ICollection<ResponseProductoDto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0);

    Task<ResponseProductoDto> FindByIdAsync(short id);

    Task<ResponseProductoDto> CreateProductoAsync(RequestProductoDto productoDTO);

    Task<ResponseProductoDto> UpdateProductoAsync(short id, RequestProductoDto productoDTO);
}