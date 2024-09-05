using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceFactura
{
    Task<ResponseFacturaDto> CreateAsync(RequestFacturaDto facturaDto, RequestPedidoDto? pedidoDto);

    Task<ICollection<ResponseFacturaDto>> ListAllAsync();

    Task<ResponseFacturaDto?> FindByIdAsync(long id);
}
