using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventarioProducto(IRepositoryInventarioProducto repository, IMapper mapper,
                                    IValidator<InventarioProducto> inventarioProductoValidator) : IServiceInventarioProducto
{
    /// <summary>
    /// Create inventory product
    /// </summary>
    /// <param name="inventarioProductoDto">Inventary product request model to be added</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    public async Task<ResponseInventarioProductoDto> CreateProductoInventarioAsync(RequestInventarioProductoDto inventarioProductoDto)
    {
        var inventarioProducto = await ValidateInventarioProductoAsync(inventarioProductoDto);

        var result = await repository.CreateProductoInventarioAsync(inventarioProducto);
        if (result == null) throw new NotFoundException("Inventario producto no creado.");

        return mapper.Map<ResponseInventarioProductoDto>(result);
    }

    /// <summary>
    /// Create inventory products
    /// </summary>
    /// <param name="inventarioProductosDto">List of Inventary product request model to be added</param>
    /// <returns>bool</returns>
    public async Task<bool> CreateProductoInventarioAsync(IEnumerable<RequestInventarioProductoDto> inventarioProductosDto)
    {
        var inventarioProductos = await ValidateInventarioProductoAsync(inventarioProductosDto);
        var result = await repository.CreateProductoInventarioAsync(inventarioProductos);
        if (!result) throw new ListNotAddedException("Error al guardar inventario productos.");

        return result;
    }

    /// <summary>
    /// Get inventory product with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    public async Task<ResponseInventarioProductoDto> FindByIdAsync(long id)
    {
        var inventarioProducto = await repository.FindByIdAsync(id);
        if (inventarioProducto == null) throw new NotFoundException("Inventario producto no encontrado.");

        return mapper.Map<ResponseInventarioProductoDto>(inventarioProducto);
    }

    /// <summary>
    /// Get list of all inventory product by inventory
    /// </summary>
    /// <param name="idInventario">Inventory id</param>
    /// <returns>ICollection of ResponseInventarioProductoDto</returns>
    public async Task<ICollection<ResponseInventarioProductoDto>> ListAllByInventarioAsync(short idInventario)
    {
        var list = await repository.ListAllByInventarioAsync(idInventario);
        var collection = mapper.Map<ICollection<ResponseInventarioProductoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Get list of all inventory product by product
    /// </summary>
    /// <param name="idProducto">Product id</param>
    /// <returns>ICollection of ResponseInventarioProductoDto</returns>
    public async Task<ICollection<ResponseInventarioProductoDto>> ListAllByProductoAsync(short idProducto)
    {
        var list = await repository.ListAllByProductoAsync(idProducto);
        var collection = mapper.Map<ICollection<ResponseInventarioProductoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Update inventory product
    /// </summary>
    /// <param name="idInventarioProducto">Inventory Product id</param>
    /// <param name="inventarioProductoDto">Inventary product request model to be updated</param>
    /// <returns>ResponseInventarioProductoDto</returns>
    public async Task<ResponseInventarioProductoDto> UpdateProductoInventarioAsync(long idInventarioProducto, RequestInventarioProductoDto inventarioProductoDto)
    {
        if (!await repository.ExistsInventarioProductoAsync(idInventarioProducto)) throw new NotFoundException("Inventario producto no encontrada.");

        var inventarioProducto = await ValidateInventarioProductoAsync(inventarioProductoDto);
        inventarioProducto.Id = idInventarioProducto;
        var result = await repository.UpdateProductoInventarioAsync(inventarioProducto);

        return mapper.Map<ResponseInventarioProductoDto>(result);
    }

    /// <summary>
    /// Validate inventory product request model
    /// </summary>
    /// <param name="inventarioProductoDto">Inventory product request model to be added</param>
    /// <returns>InventarioProducto</returns>
    private async Task<InventarioProducto> ValidateInventarioProductoAsync(RequestInventarioProductoDto inventarioProductoDto)
    {
        var inventarioProducto = mapper.Map<InventarioProducto>(inventarioProductoDto);
        await inventarioProductoValidator.ValidateAndThrowAsync(inventarioProducto);
        return inventarioProducto;
    }

    /// <summary>
    /// Validate inventory products request model
    /// </summary>
    /// <param name="inventarioProductosDto">Inventory products request model to be added</param>
    /// <returns>IEnumerable of InventarioProducto</returns>
    private async Task<IEnumerable<InventarioProducto>> ValidateInventarioProductoAsync(IEnumerable<RequestInventarioProductoDto> inventarioProductosDto)
    {
        var inventarioProductos = mapper.Map<List<InventarioProducto>>(inventarioProductosDto);
        foreach (var item in inventarioProductos)
        {
            await inventarioProductoValidator.ValidateAndThrowAsync(item);
        }
        return inventarioProductos;
    }
}