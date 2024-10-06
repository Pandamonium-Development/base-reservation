using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Enums;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventoryProductTransaction(IRepositoryInventoryProductTransaction repository, IRepositoryInventoryProduct repositoryInventoryProduct,
                                                IMapper mapper, IValidator<InventoryProductTransaction> inventoryProductTransactionValidator) : IServiceInventoryProductTransaction
{
    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductTransactionAsync(RequestInventoryProductTransactionDto inventoryProductTransactionDto)
    {
        var inventoryProductTransaction = await ValidateInventoryProductTransactionAsync(inventoryProductTransactionDto);

        var inventarioProducto = await repositoryInventoryProduct.FindByIdAsync(inventoryProductTransaction.InventoryProductId);
        if (inventarioProducto == null) throw new NotFoundException("Inventario producto no creado.");

        if (inventoryProductTransaction.TransactionType == TransactionTypeInventory.Salida && inventarioProducto.Assignable - inventoryProductTransaction.Quantity < 0)
            throw new BaseReservationException("No puede generar un movimiento de inventario con una cantidad mayor a la disponible.");

        var newAssignableQuantity = inventoryProductTransactionDto.TransactionType == Enums.TransactionTypeInventory.Entrada ?
                            inventoryProductTransaction.Quantity : inventoryProductTransaction.Quantity * -1 + inventarioProducto.Assignable;

        if (newAssignableQuantity > inventarioProducto.Maximum)
            throw new BaseReservationException("Cantidad nueva disponible excede el máximo asignado.");

        if (newAssignableQuantity < inventarioProducto.Mininum)
            throw new BaseReservationException("Cantidad nueva disponible es menor al mínimo asignado.");

        var result = await repository.CreateInventoryProductTransactionAsync(inventoryProductTransaction);
        if (!result) throw new NotFoundException("Movimiento inventario no creado.");

        return result;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByInventoryAsync(short inventoryId)
    {
        var list = await repository.ListAllByInventoryAsync(inventoryId);
        var collection = mapper.Map<ICollection<ResponseInventoryProductTransactionDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryProductTransactionDto>> ListAllByProductAsync(short productId)
    {
        var list = await repository.ListAllByProductAsync(productId);
        var collection = mapper.Map<ICollection<ResponseInventoryProductTransactionDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate inventory product movement
    /// </summary>
    /// <param name="inventoryProductTransactionDto">Inventory product movement request model to be added</param>
    /// <returns>InventoryProductTransaction</returns>
    private async Task<InventoryProductTransaction> ValidateInventoryProductTransactionAsync(RequestInventoryProductTransactionDto inventoryProductTransactionDto)
    {
        var inventoryProductTransaction = mapper.Map<InventoryProductTransaction>(inventoryProductTransactionDto);
        await inventoryProductTransactionValidator.ValidateAndThrowAsync(inventoryProductTransaction);
        return inventoryProductTransaction;
    }
}