using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceInventario
{
    Task<ICollection<ResponseInventarioDto>> ListAllAsync();

    Task<ICollection<ResponseInventarioDto>> ListAllAsync(byte idSucursal);

    Task<ResponseInventarioDto> FindByIdAsync(short id);

    Task<ResponseInventarioDto> CreateInventarioAsync(byte idSucursal, RequestInventarioDto productoDTO);

    Task<ResponseInventarioDto> UpdateInventarioAsync(byte idSucursal, short id, RequestInventarioDto productoDTO);

    Task<bool> DeleteInventarioAsync(short id);
}