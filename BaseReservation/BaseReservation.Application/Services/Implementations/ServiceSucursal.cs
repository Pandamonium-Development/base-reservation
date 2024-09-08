using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSucursal(IRepositorySucursal repository, IMapper mapper,
                                IValidator<Sucursal> sucursalValidator, IServiceUserAuthorization serviceUserAuthorization) : IServiceSucursal
{
    /// <inheritdoc />
    public async Task<ResponseSucursalDto> CreateSucursalAsync(RequestSucursalDto sucursalDTO)
    {
        var sucursal = await ValidateSucursal(sucursalDTO);

        var result = await repository.CreateSucursalAsync(sucursal);
        if (result == null) throw new NotFoundException("Sucursal no se ha creado.");

        return mapper.Map<ResponseSucursalDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseSucursalDto> UpdateSucursalAsync(byte id, RequestSucursalDto sucursalDTO)
    {
        if (!await repository.ExistsSucursalAsync(id)) throw new NotFoundException("Sucursal no encontrada.");

        var sucursal = await ValidateSucursal(sucursalDTO);
        sucursal.Id = id;
        var result = await repository.UpdateSucursalAsync(sucursal);

        return mapper.Map<ResponseSucursalDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseSucursalDto> FindByIdAsync(byte id)
    {
        var sucursal = await repository.FindByIdAsync(id);
        if (sucursal == null) throw new NotFoundException("Sucursal no encontrada.");

        return mapper.Map<ResponseSucursalDto>(sucursal);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseSucursalDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseSucursalDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    private async Task<Sucursal> ValidateSucursal(RequestSucursalDto sucursalDTO)
    {
        var sucursal = mapper.Map<Sucursal>(sucursalDTO);
        await sucursalValidator.ValidateAndThrowAsync(sucursal);
        return sucursal;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseSucursalDto>> ListAllByRolAsync()
    {
        var usuario = await serviceUserAuthorization.GetLoggedUser();

        var list = await repository.ListAllByRoleAsync(usuario.Rol.Descripcion);
        var collection = mapper.Map<ICollection<ResponseSucursalDto>>(list);

        return collection;
    }
}