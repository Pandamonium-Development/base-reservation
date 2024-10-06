using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceProduct(IRepositoryProduct repository, IMapper mapper,
                                  IValidator<Product> productValidator) : IServiceProduct
{
    /// <inheritdoc />
    public async Task<ResponseProductDto> CreateProductAsync(RequestProductDto productDTO)
    {
        var product = await ValidateProductAsync(productDTO);

        var result = await repository.CreateProductAsync(product);
        if (result == null) throw new NotFoundException("Producto no creado.");

        return mapper.Map<ResponseProductDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseProductDto> UpdateProductAsync(short id, RequestProductDto productDTO)
    {
        if (!await repository.ExistsProductAsync(id)) throw new NotFoundException("Product no encontrada.");

        var product = await ValidateProductAsync(productDTO);
        product.Id = id;
        var result = await repository.UpdateProductAsync(product);

        return mapper.Map<ResponseProductDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseProductDto> FindByIdAsync(short id)
    {
        var product = await repository.FindByIdAsync(id);
        if (product == null) throw new NotFoundException("Producto no encontrado.");

        return mapper.Map<ResponseProductDto>(product);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProductDto>> ListAllAsync(bool excludeProductsInventory = false, short inventoryId = 0)
    {
        var list = await repository.ListAllAsync(excludeProductsInventory, inventoryId);
        var collection = mapper.Map<ICollection<ResponseProductDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate product
    /// </summary>
    /// <param name="productDTO">Product request model to be added/updated</param>
    /// <returns>Product</returns>
    private async Task<Product> ValidateProductAsync(RequestProductDto productDTO)
    {
        var product = mapper.Map<Product>(productDTO);
        await productValidator.ValidateAndThrowAsync(product);
        return product;
    }
}