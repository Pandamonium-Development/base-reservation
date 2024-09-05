using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUsuarioSucursal
{
    Task<bool> AssignEncargados(byte idSucursal, IEnumerable<RequestUsuarioSucursalDto> usuariosSucursalDto);
}