using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetallePedido
{
    Task<ResponseDetallePedidoDto> FindByIdAsync(long id);

    Task<ICollection<ResponseDetallePedidoDto>> ListAllByPedidoAsync(long idPedido);
}
