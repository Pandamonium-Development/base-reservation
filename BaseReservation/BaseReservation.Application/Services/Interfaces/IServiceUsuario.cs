using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUsuario
{
    Task<ICollection<ResponseUsuarioDto>> ListAllAsync(string? rol = null);

    Task<ResponseUsuarioDto> FindByIdAsync(short id);

    Task<bool> FreeAssignmentSucursalAsync(short id, byte idSucursalAsignacion);
}