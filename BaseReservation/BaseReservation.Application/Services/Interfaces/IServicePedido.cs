using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServicePedido
{
    Task<ResponsePedidoDto> CreatePedidoAsync(RequestPedidoDto pedidoDto);

    Task<ICollection<ResponsePedidoDto>> ListAllAsync();

    Task<ResponsePedidoDto> FindByIdAsync(long id);
}