using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceOrder(IRepositoryOrder repository, IServiceReservation serviceReservation,
                            IMapper mapper, IValidator<Order> orderValidator) : IServiceOrder
{
    /// <inheritdoc />
    public async Task<ResponseOrderDto> CreateOrderAsync(RequestOrderDto orderDto)
    {
        var order = await ValidateOrderAsync(orderDto);

        if (!await serviceReservation.ExistsReservationAsync(orderDto.ReservationId)) throw new NotFoundException("Reserva no existe.");
        var reservation = await serviceReservation.FindByIdAsync(orderDto.ReservationId);
        reservation!.Status = "A";
        order.BranchId = reservation!.BranchId;

        var result = await repository.CreateOrderAsync(order, mapper.Map<Reservation>(reservation));
        if (result == null) throw new NotFoundException("Pedido no creado.");

        return mapper.Map<ResponseOrderDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseOrderDto> FindByIdAsync(long id)
    {
        var order = await repository.FindByIdAsync(id);
        if (order == null) throw new NotFoundException("Proforma no encontrada.");

        return mapper.Map<ResponseOrderDto>(order);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseOrderDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseOrderDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate order
    /// </summary>
    /// <param name="orderDto">Order request model to be added</param>
    /// <returns>Order</returns>
    private async Task<Order> ValidateOrderAsync(RequestOrderDto orderDto)
    {
        var order = mapper.Map<Order>(orderDto);
        await orderValidator.ValidateAndThrowAsync(order);

        return order;
    }
}