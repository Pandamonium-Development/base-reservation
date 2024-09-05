using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursalFeriado
{
    Task<ICollection<ResponseSucursalFeriadoDto>> GetFeriadosBySucursalAsync(byte idSucursal, short? anno);

    Task<ResponseSucursalFeriadoDto?> GetSucursalFeriadoByIdAsync(short id);

    Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<RequestSucursalFeriadoDto> sucursalFeriados);
}