using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceFactura(IRepositoryFactura repository, IRepositoryPedido repositoryPedido, IServicePedido servicePedido,
                            IMapper mapper, IValidator<Factura> facturaValidator) : IServiceFactura
{
    /// <inheritdoc />
    public async Task<ResponseFacturaDto> CreateAsync(RequestFacturaDto facturaDto)
    {
        var factura = await ValidarFactura(facturaDto);

        ResponsePedidoDto? pedido = null;
        if (facturaDto.IdPedido != null && await repositoryPedido.ExistsPedidoAsync(facturaDto.IdPedido.Value))
        {
            pedido = await servicePedido.FindByIdAsync(facturaDto.IdPedido.Value);
            pedido.Estado = 'F';
            facturaDto.IdSucursal = pedido.IdSucursal;
        }

        var result = await repository.CreateAsync(factura, mapper.Map<Pedido>(pedido));
        if (result == null) throw new NotFoundException("Factura no creada.");

        return mapper.Map<ResponseFacturaDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseFacturaDto> FindByIdAsync(long id)
    {
        var factura = await repository.FindByIdAsync(id);
        if (factura == null) throw new NotFoundException("Factura no encontrada.");

        return mapper.Map<ResponseFacturaDto>(factura);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseFacturaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        list = list.OrderByDescending(x => x.Fecha).ToList();
        var collection = mapper.Map<ICollection<ResponseFacturaDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validates the provided Factura data and returns a mapped Factura entity.
    /// </summary>
    /// <param name="facturaDto">The data transfer object containing the Factura information to validate.</param>
    /// <returns>RequestFacturaDto</returns>
    private async Task<Factura> ValidarFactura(RequestFacturaDto facturaDto)
    {
        var factura = mapper.Map<Factura>(facturaDto);
        await facturaValidator.ValidateAndThrowAsync(factura);
        return factura;
    }
}
