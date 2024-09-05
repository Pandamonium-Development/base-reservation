using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUsuario(IRepositoryUsuario repository, IRepositorySucursal repositorySucursal, IMapper mapper) : IServiceUsuario
{
    public async Task<ResponseUsuarioDto> FindByIdAsync(short id)
    {
        var usuario = await repository.FindByIdAsync(id);
        if (usuario == null) throw new NotFoundException("Usuario no encontrado.");

        return mapper.Map<ResponseUsuarioDto>(usuario);
    }

    public async Task<bool> FreeAssignmentSucursalAsync(short id, byte idSucursalAsignacion)
    {
        var usuario = await repository.ExistsUsuarioAsync(id);
        if (!usuario) throw new NotFoundException("Usuario no encontrado.");

        var sucursal = await repositorySucursal.ExistsSucursalAsync(idSucursalAsignacion);
        if (!sucursal) throw new NotFoundException("Sucursal no encontrada.");

        return await repository.IsAvailableAsync(id, idSucursalAsignacion);
    }

    public async Task<ICollection<ResponseUsuarioDto>> ListAllAsync(string? rol = null)
    {
        if (rol == null)
        {
            var list = await repository.ListAllAsync();
            return mapper.Map<ICollection<ResponseUsuarioDto>>(list);
        }

        Rol rolEnum;
        if (!Enum.TryParse(rol, out rolEnum)) throw new BaseReservationException("Rol Inválido");

        var listFilter = await repository.ListAllByRoleAsync((byte)rolEnum);
        var collection = mapper.Map<ICollection<ResponseUsuarioDto>>(listFilter);

        return collection;
    }

}