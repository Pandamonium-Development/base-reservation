using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventarioProductoMovimiento
{
    Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByInventarioAsync(short idInventario);

    Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByProductoAsync(short idProducto);

    Task<bool> CreateInventarioMovimientoProductoAsync(RequestInventarioProductoMovimientoDto inventarioProductoMovimientoDto);
}