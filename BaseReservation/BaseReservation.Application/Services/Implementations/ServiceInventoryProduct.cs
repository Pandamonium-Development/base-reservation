using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventoryProduct(IRepositoryInventoryProduct repository, IMapper mapper,
                                    IValidator<InventoryProduct> inventoryProductValidator) : IServiceInventoryProduct
{
    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> CreateInventoryProductAsync(RequestInventoryProductDto inventoryProductDto)
    {
        var inventoryProduct = await ValidateInventoryProductAsync(inventoryProductDto);

        var result = await repository.CreateInventoryProductAsync(inventoryProduct);
        if (result == null) throw new NotFoundException("Inventario producto no creado.");

        return mapper.Map<ResponseInventoryProductDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductAsync(IEnumerable<RequestInventoryProductDto> inventoryProductsDto)
    {
        var inventoryProducts = await ValidateInventoryProductAsync(inventoryProductsDto);
        var result = await repository.CreateInventoryProductAsync(inventoryProducts);
        if (!result) throw new ListNotAddedException("Error al guardar inventario productos.");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> FindByIdAsync(long id)
    {
        var inventoryProduct = await repository.FindByIdAsync(id);
        if (inventoryProduct == null) throw new NotFoundException("Inventario producto no encontrado.");

        return mapper.Map<ResponseInventoryProductDto>(inventoryProduct);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductDto>> ListAllByInventoryAsync(short inventoryId)
    {
        var list = await repository.ListAllByInventoryAsync(inventoryId);
        var collection = mapper.Map<ICollection<ResponseInventoryProductDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductDto>> ListAllByProductAsync(short productId)
    {
        var list = await repository.ListAllByProductAsync(productId);
        var collection = mapper.Map<ICollection<ResponseInventoryProductDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryProductDto> UpdateInventoryProductAsync(long inventoryProductId, RequestInventoryProductDto inventoryProductDto)
    {
        if (!await repository.ExistsInventoryProductAsync(inventoryProductId)) throw new NotFoundException("Inventario producto no encontrada.");

        var inventoryProduct = await ValidateInventoryProductAsync(inventoryProductDto);
        inventoryProduct.Id = inventoryProductId;
        var result = await repository.UpdateInventoryProductAsync(inventoryProduct);

        return mapper.Map<ResponseInventoryProductDto>(result);
    }

    /// <summary>
    /// Validate inventory product request model
    /// </summary>
    /// <param name="inventoryProductDto">Inventory product request model to be added</param>
    /// <returns>InventoryProduct</returns>
    private async Task<InventoryProduct> ValidateInventoryProductAsync(RequestInventoryProductDto inventoryProductDto)
    {
        var inventoryProduct = mapper.Map<InventoryProduct>(inventoryProductDto);
        await inventoryProductValidator.ValidateAndThrowAsync(inventoryProduct);
        return inventoryProduct;
    }

    /// <summary>
    /// Validate inventory products request model
    /// </summary>
    /// <param name="inventoryProductsDto">Inventory products request model to be added</param>
    /// <returns>IEnumerable of InventoryProduct</returns>
    private async Task<IEnumerable<InventoryProduct>> ValidateInventoryProductAsync(IEnumerable<RequestInventoryProductDto> inventoryProductsDto)
    {
        var inventoryProducts = mapper.Map<List<InventoryProduct>>(inventoryProductsDto);
        foreach (var item in inventoryProducts)
        {
            await inventoryProductValidator.ValidateAndThrowAsync(item);
        }
        return inventoryProducts;
    }
}