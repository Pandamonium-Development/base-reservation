using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUsuarioSucursal(IRepositoryUsuarioSucursal repository, IMapper mapper, IValidator<UsuarioSucursal> usuarioSucursalValidator) : IServiceUsuarioSucursal
{

    public async Task<bool> AssignEncargadosAsync(byte idSucursal, IEnumerable<RequestUsuarioSucursalDto> usuariosSucursalDto)
    {
        var usuariosSucursal = await ValidateUsuariosSucursalAsync(idSucursal, usuariosSucursalDto);
        return await repository.AssignUsuariosAsync(idSucursal, usuariosSucursal);
    }

    private async Task<IEnumerable<UsuarioSucursal>> ValidateUsuariosSucursalAsync(byte idSucursal, IEnumerable<RequestUsuarioSucursalDto> usuariosSucursal)
    {
        var usuariosSucursalExistentes = mapper.Map<List<UsuarioSucursal>>(usuariosSucursal);
        foreach (var item in usuariosSucursalExistentes)
        {
            item.IdSucursal = idSucursal;
            await usuarioSucursalValidator.ValidateAndThrowAsync(item);
        }
        return usuariosSucursalExistentes;
    }
}