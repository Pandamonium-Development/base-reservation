using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceOrder
{
    /// <summary>
    /// Create order
    /// </summary>
    /// <param name="orderDto">Order request model to be added</param>
    /// <returns>ResponseOrderDto</returns>
    Task<ResponseOrderDto> CreateOrderAsync(RequestOrderDto orderDto);

    /// <summary>
    /// Get list of all orders
    /// </summary>
    /// <returns>ICollection of ResponseOrderDto</returns>
    Task<ICollection<ResponseOrderDto>> ListAllAsync();

    /// <summary>
    /// Get Order by Id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseOrderDto</returns>
    Task<ResponseOrderDto> FindByIdAsync(long id);
}