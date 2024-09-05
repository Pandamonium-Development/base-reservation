using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursal
{
    Task<ICollection<ResponseSucursalDto>> ListAllAsync();

    Task<ICollection<ResponseSucursalDto>> ListAllByRolAsync();

    Task<ResponseSucursalDto> FindByIdAsync(byte id);

    Task<ResponseSucursalDto> CreateSucursalAsync(RequestSucursalDto sucursalDTO);

    Task<ResponseSucursalDto> UpdateSucursalAsync(byte id, RequestSucursalDto sucursalDTO);
}