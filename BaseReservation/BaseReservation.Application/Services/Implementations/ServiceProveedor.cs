using BaseReservation.Application.Comunes;
using BaseReservation.Application.Configuration.Pagination;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceProveedor(IRepositoryProveedor repository, IMapper mapper, IValidator<Proveedor> proveedorValidator) : IServiceProveedor
{
    /// <summary>
    /// Create vendor
    /// </summary>
    /// <param name="proveedorDto">Vendor request model to be added</param>
    /// <returns>ResponseProveedorDto</returns>
    public async Task<ResponseProveedorDto> CreateProveedorAsync(RequestProveedorDto proveedorDto)
    {
        var proveedor = await ValidateProveedorAsync(proveedorDto);

        var result = await repository.CreateProveedorAsync(proveedor);
        if (result == null) throw new NotFoundException("Proveedor no creado.");
        return mapper.Map<ResponseProveedorDto>(result);
    }

    /// <summary>
    /// Delete existing vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <returns>bool</returns>
    public async Task<bool> DeleteProveedorsyncAsync(byte id)
    {
        if (!await repository.ExistsProveedorAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        return await repository.DeleteProveedorAsync(id);
    }

    /// <summary>
    /// Get vendor with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProveedorDto</returns>
    public async Task<ResponseProveedorDto> FindByIdAsync(byte id)
    {
        var proveedor = await repository.FindByIdAsync(id);
        if (proveedor == null) throw new NotFoundException("Proveedor no encontrado.");

        return mapper.Map<ResponseProveedorDto>(proveedor);
    }

    /// <summary>
    /// Get list of all vendors
    /// </summary>
    /// <returns>ICollection of ResponseProveedorDto</returns>
    public async Task<ICollection<ResponseProveedorDto>> ListAllAsync()
    {
        var proveedores = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseProveedorDto>>(proveedores);
    }

    /// <summary>
    /// Get list of all vendors paginated
    /// </summary>
    /// <param name="paginationParameters">Pagination paramets options</param>
    /// <returns>PagedList of ResponseProveedorDto</returns>
    public async Task<PagedList<ResponseProveedorDto>> ListAllAsync(PaginationParameters paginationParameters)
    {
        var query = repository.ListAllQueryable();
        var paginatedCollection = await PagedList<Proveedor>.PaginatedCollection(query, paginationParameters.PageNumber, paginationParameters.PageSize);
        var proveedores = mapper.Map<ICollection<ResponseProveedorDto>>(paginatedCollection);
        var count = await query.CountAsync();

        return PagedList<ResponseProveedorDto>.ToPagedList(proveedores.ToList(), count, paginationParameters.PageNumber, paginationParameters.PageSize);
    }

    /// <summary>
    /// Update vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <param name="proveedorDto">Vendor request model to be updated</param>
    /// <returns>ResponseProveedorDto</returns>
    public async Task<ResponseProveedorDto> UpdateProveedorsync(byte id, RequestProveedorDto proveedorDto)
    {
        if (!await repository.ExistsProveedorAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        var proveedor = await ValidateProveedorAsync(proveedorDto);
        proveedor.Id = id;
        var result = await repository.UpdateProveedorAsync(proveedor);

        return mapper.Map<ResponseProveedorDto>(result);
    }

    /// <summary>
    /// Validate vendor
    /// </summary>
    /// <param name="proveedorDto">Vendor request model to be added/updated</param>
    /// <returns>Proveedor</returns>
    private async Task<Proveedor> ValidateProveedorAsync(RequestProveedorDto proveedorDto)
    {
        var proveedor = mapper.Map<Proveedor>(proveedorDto);
        await proveedorValidator.ValidateAndThrowAsync(proveedor);
        return proveedor;
    }
}