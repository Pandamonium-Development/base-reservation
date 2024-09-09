using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServicePedido(IRepositoryPedido repository, IRepositoryReserva repositoryReserva, IServiceReserva serviceReserva,
                            IMapper mapper, IValidator<Pedido> pedidoValidator) : IServicePedido
{
    /// <inheritdoc />
    public async Task<ResponsePedidoDto> CreatePedidoAsync(RequestPedidoDto pedidoDto)
    {
        var pedido = await ValidatePedidoAsync(pedidoDto);

        if (!await repositoryReserva.ExistsReservaAsync(pedidoDto.IdReserva)) throw new NotFoundException("Reserva no existe.");
        var reserva = await serviceReserva.FindByIdAsync(pedidoDto.IdReserva);
        reserva!.Estado = "A";
        pedido.IdSucursal = reserva!.IdSucursal;

        var result = await repository.CreatePedidoAsync(pedido, mapper.Map<Reserva>(reserva));
        if (result == null) throw new NotFoundException("Pedido no creado.");

        return mapper.Map<ResponsePedidoDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponsePedidoDto> FindByIdAsync(long id)
    {
        var pedido = await repository.FindByIdAsync(id);
        if (pedido == null) throw new NotFoundException("Proforma no encontrada.");

        return mapper.Map<ResponsePedidoDto>(pedido);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponsePedidoDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponsePedidoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate order
    /// </summary>
    /// <param name="pedidoDto">Order request model to be added</param>
    /// <returns>Pedido</returns>
    private async Task<Pedido> ValidatePedidoAsync(RequestPedidoDto pedidoDto)
    {
        var pedido = mapper.Map<Pedido>(pedidoDto);
        await pedidoValidator.ValidateAndThrowAsync(pedido);

        return pedido;
    }
}