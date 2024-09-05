using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventarioProductoMovimiento(IRepositoryInventarioProductoMovimiento repository, IRepositoryInventarioProducto repositoryInventarioProducto,
                                                IMapper mapper, IValidator<InventarioProductoMovimiento> inventarioProductoMovimientoValidator) : IServiceInventarioProductoMovimiento
{
    /// <summary>
    /// Create inventory product movement
    /// </summary>
    /// <param name="inventarioProductoMovimientoDto">Inventory product movement to be added</param>
    /// <returns>bool</returns>
    public async Task<bool> CreateInventarioMovimientoProductoAsync(RequestInventarioProductoMovimientoDto inventarioProductoMovimientoDto)
    {
        var inventarioProductoMovimiento = await ValidateInventarioProductoMovimientoAsync(inventarioProductoMovimientoDto);

        var inventarioProducto = await repositoryInventarioProducto.FindByIdAsync(inventarioProductoMovimiento.IdInventarioProducto);
        if (inventarioProducto == null) throw new NotFoundException("Inventario producto no creado.");

        if (inventarioProductoMovimiento.TipoMovimiento == TipoMovimientoInventario.Salida && inventarioProducto.Disponible - inventarioProductoMovimiento.Cantidad < 0)
            throw new BaseReservationException("No puede generar un movimiento de inventario con una cantidad mayor a la disponible.");

        var nuevaCantidadDisponible = inventarioProductoMovimientoDto.TipoMovimiento == ResponseDTOs.Enums.TipoMovimientoInventario.Entrada ?
                            inventarioProductoMovimiento.Cantidad : inventarioProductoMovimiento.Cantidad * -1 + inventarioProducto.Disponible;

        if (nuevaCantidadDisponible > inventarioProducto.Maxima)
            throw new BaseReservationException("Cantidad nueva disponible excede el máximo asignado.");

        if (nuevaCantidadDisponible < inventarioProducto.Minima)
            throw new BaseReservationException("Cantidad nueva disponible es menor al mínimo asignado.");

        var result = await repository.CreateInventarioMovimientoProductoAsync(inventarioProductoMovimiento);
        if (!result) throw new NotFoundException("Movimiento inventario no creado.");

        return result;
    }

    /// <summary>
    /// Get list of all inventory product movements by inventory
    /// </summary>
    /// <param name="idInventario">Inventory id</param>
    /// <returns>ICollection of ResponseInventarioProductoMovimientoDto</returns>
    public async Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByInventarioAsync(short idInventario)
    {
        var list = await repository.ListAllByInventarioAsync(idInventario);
        var collection = mapper.Map<ICollection<ResponseInventarioProductoMovimientoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Get list of all inventory product movements by product
    /// </summary>
    /// <param name="idProducto">Product id</param>
    /// <returns>ICollection of ResponseInventarioProductoMovimientoDto</returns>
    public async Task<ICollection<ResponseInventarioProductoMovimientoDto>> ListAllByProductoAsync(short idProducto)
    {
        var list = await repository.ListAllByProductoAsync(idProducto);
        var collection = mapper.Map<ICollection<ResponseInventarioProductoMovimientoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate inventory product movement
    /// </summary>
    /// <param name="inventarioProductoMovimientoDto">Inventory product movement request model to be added</param>
    /// <returns>InventarioProductoMovimiento</returns>
    private async Task<InventarioProductoMovimiento> ValidateInventarioProductoMovimientoAsync(RequestInventarioProductoMovimientoDto inventarioProductoMovimientoDto)
    {
        var inventarioProductoMovimiento = mapper.Map<InventarioProductoMovimiento>(inventarioProductoMovimientoDto);
        await inventarioProductoMovimientoValidator.ValidateAndThrowAsync(inventarioProductoMovimiento);
        return inventarioProductoMovimiento;
    }
}