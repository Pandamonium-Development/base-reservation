using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceProducto(IRepositoryProducto repository, IMapper mapper,
                                  IValidator<Producto> productoValidator) : IServiceProducto
{
    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="productoDTO">Product request model to be added</param>
    /// <returns>ResponseProductoDto</returns>
    public async Task<ResponseProductoDto> CreateProductoAsync(RequestProductoDto productoDTO)
    {
        var producto = await ValidateProductoAsync(productoDTO);

        var result = await repository.CreateProductoAsync(producto);
        if (result == null) throw new NotFoundException("Producto no creado.");

        return mapper.Map<ResponseProductoDto>(result);
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="id">Product if</param>
    /// <param name="productoDTO">Product request model to be updated</param>
    /// <returns>ResponseProductoDto</returns>
    public async Task<ResponseProductoDto> UpdateProductoAsync(short id, RequestProductoDto productoDTO)
    {
        if (!await repository.ExistsProductoAsync(id)) throw new NotFoundException("Producto no encontrada.");

        var producto = await ValidateProductoAsync(productoDTO);
        producto.Id = id;
        var result = await repository.UpdateProductoAsync(producto);

        return mapper.Map<ResponseProductoDto>(result);
    }

    /// <summary>
    /// Get producto with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProductoDto</returns>
    public async Task<ResponseProductoDto> FindByIdAsync(short id)
    {
        var producto = await repository.FindByIdAsync(id);
        if (producto == null) throw new NotFoundException("Producto no encontrado.");

        return mapper.Map<ResponseProductoDto>(producto);
    }

    /// <summary>
    /// Get list of all products
    /// </summary>
    /// <param name="excludeProductosInventario">Indicator to excludes products from specific inventory</param>
    /// <param name="idInventario">Inventory id to be excluded</param>
    /// <returns>ICollection of ResponseProductoDto</returns>
    public async Task<ICollection<ResponseProductoDto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0)
    {
        var list = await repository.ListAllAsync(excludeProductosInventario, idInventario);
        var collection = mapper.Map<ICollection<ResponseProductoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate product
    /// </summary>
    /// <param name="productoDTO">Product request model to be added/updated</param>
    /// <returns>Producto</returns>
    private async Task<Producto> ValidateProductoAsync(RequestProductoDto productoDTO)
    {
        var producto = mapper.Map<Producto>(productoDTO);
        await productoValidator.ValidateAndThrowAsync(producto);
        return producto;
    }
}