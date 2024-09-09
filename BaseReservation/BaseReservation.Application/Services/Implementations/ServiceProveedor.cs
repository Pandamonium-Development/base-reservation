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
    /// <inheritdoc />
    public async Task<ResponseProveedorDto> CreateProveedorAsync(RequestProveedorDto proveedorDto)
    {
        var proveedor = await ValidateProveedorAsync(proveedorDto);

        var result = await repository.CreateProveedorAsync(proveedor);
        if (result == null) throw new NotFoundException("Proveedor no creado.");
        return mapper.Map<ResponseProveedorDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProveedorsyncAsync(byte id)
    {
        if (!await repository.ExistsProveedorAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        return await repository.DeleteProveedorAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseProveedorDto> FindByIdAsync(byte id)
    {
        var proveedor = await repository.FindByIdAsync(id);
        if (proveedor == null) throw new NotFoundException("Proveedor no encontrado.");

        return mapper.Map<ResponseProveedorDto>(proveedor);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseProveedorDto>> ListAllAsync()
    {
        var proveedores = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseProveedorDto>>(proveedores);
    }

    /// <inheritdoc />
    public async Task<PagedList<ResponseProveedorDto>> ListAllAsync(PaginationParameters paginationParameters)
    {
        var query = repository.ListAllQueryable();
        var paginatedCollection = await PagedList<Proveedor>.PaginatedCollection(query, paginationParameters.PageNumber, paginationParameters.PageSize);
        var proveedores = mapper.Map<ICollection<ResponseProveedorDto>>(paginatedCollection);
        var count = await query.CountAsync();

        return PagedList<ResponseProveedorDto>.ToPagedList(proveedores.ToList(), count, paginationParameters.PageNumber, paginationParameters.PageSize);
    }

    /// <inheritdoc />
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