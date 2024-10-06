using BaseReservation.Application.Common;
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

public class ServiceVendor(IRepositoryVendor repository, IMapper mapper, IValidator<Vendor> vendorValidator) : IServiceVendor
{
    /// <inheritdoc />
    public async Task<ResponseVendorDto> CreateVendorAsync(RequestVendorDto vendorDto)
    {
        var vendor = await ValidateVendorAsync(vendorDto);

        var result = await repository.CreateVendorAsync(vendor);
        if (result == null) throw new NotFoundException("Proveedor no creado.");
        return mapper.Map<ResponseVendorDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteVendorAsync(byte id)
    {
        if (!await repository.ExistsVendorAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        return await repository.DeleteVendorAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseVendorDto> FindByIdAsync(byte id)
    {
        var vendor = await repository.FindByIdAsync(id);
        if (vendor == null) throw new NotFoundException("Proveedor no encontrado.");

        return mapper.Map<ResponseVendorDto>(vendor);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseVendorDto>> ListAllAsync()
    {
        var vendors = await repository.ListAllAsync();
        return mapper.Map<ICollection<ResponseVendorDto>>(vendors);
    }

    /// <inheritdoc />
    public async Task<PagedList<ResponseVendorDto>> ListAllAsync(PaginationParameters paginationParameters)
    {
        var query = repository.ListAllQueryable();
        var paginatedCollection = await PagedList<Vendor>.PaginatedCollection(query, paginationParameters.PageNumber, paginationParameters.PageSize);
        var vendors = mapper.Map<ICollection<ResponseVendorDto>>(paginatedCollection);
        var count = await query.CountAsync();

        return PagedList<ResponseVendorDto>.ToPagedList(vendors.ToList(), count, paginationParameters.PageNumber, paginationParameters.PageSize);
    }

    /// <inheritdoc />
    public async Task<ResponseVendorDto> UpdateVendorAsync(byte id, RequestVendorDto vendorDto)
    {
        if (!await repository.ExistsVendorAsync(id)) throw new NotFoundException("Proveedor no encontrada.");

        var vendor = await ValidateVendorAsync(vendorDto);
        vendor.Id = id;
        var result = await repository.UpdateVendorAsync(vendor);

        return mapper.Map<ResponseVendorDto>(result);
    }

    /// <summary>
    /// Validate vendor
    /// </summary>
    /// <param name="vendorDto">Vendor request model to be added/updated</param>
    /// <returns>Vendor</returns>
    private async Task<Vendor> ValidateVendorAsync(RequestVendorDto vendorDto)
    {
        var vendor = mapper.Map<Vendor>(vendorDto);
        await vendorValidator.ValidateAndThrowAsync(vendor);
        return vendor;
    }
}