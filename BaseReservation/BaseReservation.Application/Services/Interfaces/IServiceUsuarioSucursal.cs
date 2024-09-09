using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUsuarioSucursal
{
    /// <summary>
    /// Create branch's users 
    /// </summary>
    /// <param name="idSucursal">Branch id that receives list of users</param>
    /// <param name="usuariosSucursalDto">List of branch's users</param>
    /// <returns>bool</returns>
    Task<bool> CreateUsuarioSucursalAsync(byte idSucursal, IEnumerable<RequestUsuarioSucursalDto> usuariosSucursalDto);
}