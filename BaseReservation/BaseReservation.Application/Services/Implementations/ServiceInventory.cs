using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventory(IRepositoryInventory repository, IMapper mapper, IValidator<Inventory> inventoryValidator) : IServiceInventory
{
    /// <inheritdoc />
    public async Task<ResponseInventoryDto> CreateInventoryAsync(byte branchId, RequestInventoryDto inventoryDto)
    {
        var inventory = await ValidateInventoryAsync(inventoryDto);
        inventory.BranchId = branchId;

        var result = await repository.CreateInventoryAsync(inventory);
        if (result == null) throw new NotFoundException("Inventario no creado.");

        return mapper.Map<ResponseInventoryDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteInventoryAsync(short id)
    {
        if (!await repository.ExistsInventoryAsync(id)) throw new NotFoundException("Inventario no encontrada.");

        var inventory = await FindByIdAsync(id);
        if (inventory!.InventoryProducts.Any(m => m.Assignable != 0)) throw new BaseReservationException("No puede eliminar un inventario con productos disponibles, asegurese que todos los productos tengan cantidad 0 antes de eliminar el inventario");

        return await repository.DeleteInventoryAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryDto> FindByIdAsync(short id)
    {
        var inventory = await repository.FindByIdAsync(id);
        if (inventory == null) throw new NotFoundException("Inventario no encontrado.");

        return mapper.Map<ResponseInventoryDto>(inventory);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseInventoryDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseInventoryDto>> ListAllByBranchAsync(byte branchId)
    {
        var list = await repository.ListAllByBranchAsync(branchId);
        var collection = mapper.Map<ICollection<ResponseInventoryDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<ResponseInventoryDto> UpdateInventoryAsync(byte branchId, short id, RequestInventoryDto inventoryDto)
    {
        if (!await repository.ExistsInventoryAsync(id)) throw new NotFoundException("Inventario no encontrada.");

        var inventory = await ValidateInventoryAsync(inventoryDto);
        inventory.BranchId = branchId;
        inventory.Id = id;
        var result = await repository.UpdateInventoryAsync(inventory);

        return mapper.Map<ResponseInventoryDto>(result);
    }

    /// <summary>
    /// Validate if inventary could be Mapped to be added/updated
    /// </summary>
    /// <param name="inventoryDto">Inventory model to be validated</param>
    /// <returns>Inventory</returns>
    private async Task<Inventory> ValidateInventoryAsync(RequestInventoryDto inventoryDto)
    {
        var inventory = mapper.Map<Inventory>(inventoryDto);
        await inventoryValidator.ValidateAndThrowAsync(inventory);
        return inventory;
    }
}