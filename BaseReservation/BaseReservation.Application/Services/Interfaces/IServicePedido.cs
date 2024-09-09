using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServicePedido
{
    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="pedidoDto">Order request model to be added</param>
    /// <returns>ResponsePedidoDto</returns>
    Task<ResponsePedidoDto> CreatePedidoAsync(RequestPedidoDto pedidoDto);

    /// <summary>
    /// Get list of all orders
    /// </summary>
    /// <returns>ICollection of ResponsePedidoDto</returns>
    Task<ICollection<ResponsePedidoDto>> ListAllAsync();

    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponsePedidoDto</returns>
    Task<ResponsePedidoDto> FindByIdAsync(long id);
}