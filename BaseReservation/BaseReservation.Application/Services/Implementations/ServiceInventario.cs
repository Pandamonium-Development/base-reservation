using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceInventario(IRepositoryInventario repository, IMapper mapper, IValidator<Inventario> inventarioValidator) : IServiceInventario
{
    /// <summary>
    /// Create inventario
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="inventarioDto">Inventory model request to be added</param>
    /// <returns>ResponseInventarioDto</returns>
    public async Task<ResponseInventarioDto> CreateInventarioAsync(byte idSucursal, RequestInventarioDto inventarioDto)
    {
        var inventario = await ValidateInventarioAsync(inventarioDto);
        inventario.IdSucursal = idSucursal;

        var result = await repository.CreateInventarioAsync(inventario);
        if (result == null) throw new NotFoundException("Inventario no creado.");

        return mapper.Map<ResponseInventarioDto>(result);
    }

    /// <summary>
    /// Delete inventory
    /// </summary>
    /// <param name="id">Inventory id to be deleted</param>
    /// <returns>True if was deleted successfully, if not, false</returns>
    public async Task<bool> DeleteInventarioAsync(short id)
    {
        if (!await repository.ExistsInventarioAsync(id)) throw new NotFoundException("Inventario no encontrada.");

        var inventario = await FindByIdAsync(id);
        if (inventario!.InventarioProductos.Any(m => m.Disponible != 0)) throw new BaseReservationException("No puede eliminar un inventario con productos disponibles, asegurese que todos los productos tengan cantidad 0 antes de eliminar el inventario");

        return await repository.DeleteInventarioAsync(id);
    }

    /// <summary>
    /// Finds Inventory by its unique identifier
    /// </summary>
    /// <param name="id">The Inventory entity id</param>
    /// <returns>ResponseInventarioDto if founded, otherwise null</returns>
    public async Task<ResponseInventarioDto> FindByIdAsync(short id)
    {
        var inventario = await repository.FindByIdAsync(id);
        if (inventario == null) throw new NotFoundException("Inventario no encontrado.");

        return mapper.Map<ResponseInventarioDto>(inventario);
    }

    /// <summary>
    /// Get list of all inventories
    /// </summary>
    /// <returns>ICollection of ResponseInventarioDto</returns>
    public async Task<ICollection<ResponseInventarioDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseInventarioDto>>(list);

        return collection;
    }

    /// <summary>
    /// Get list of all inventories by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of ResponseInventarioDto</returns>
    public async Task<ICollection<ResponseInventarioDto>> ListAllBySucursalAsync(byte idSucursal)
    {
        var list = await repository.ListAllBySucursalAsync(idSucursal);
        var collection = mapper.Map<ICollection<ResponseInventarioDto>>(list);

        return collection;
    }

    /// <summary>
    /// Update existing inventarory
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="id">Inventary id</param>
    /// <param name="inventarioDto">Inventory model request to be updated</param>
    /// <returns>ResponseInventarioDto</returns>
    public async Task<ResponseInventarioDto> UpdateInventarioAsync(byte idSucursal, short id, RequestInventarioDto inventarioDto)
    {
        if (!await repository.ExistsInventarioAsync(id)) throw new NotFoundException("Inventario no encontrada.");

        var inventario = await ValidateInventarioAsync(inventarioDto);
        inventario.IdSucursal = idSucursal;
        inventario.Id = id;
        var result = await repository.UpdateInventarioAsync(inventario);

        return mapper.Map<ResponseInventarioDto>(result);
    }

    /// <summary>
    /// Validate if inventary could be Mapped to be added/updated
    /// </summary>
    /// <param name="inventarioDto">Inventory model to be validated</param>
    /// <returns>Inventario</returns>
    private async Task<Inventario> ValidateInventarioAsync(RequestInventarioDto inventarioDto)
    {
        var inventario = mapper.Map<Inventario>(inventarioDto);
        await inventarioValidator.ValidateAndThrowAsync(inventario);
        return inventario;
    }
}