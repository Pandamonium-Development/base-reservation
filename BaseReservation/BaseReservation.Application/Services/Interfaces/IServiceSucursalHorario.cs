using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces
{
    public interface IServiceSucursalHorario
    {
        Task<ICollection<ResponseSucursalHorarioDto>> GetHorariosBySucursalAsync(byte idSucursal);

        Task<ResponseSucursalHorarioDto> GetSucursalHorarioByIdAsync(short id);

        Task<bool> CreateSucursalHorarioAsync(byte idSucursal, IEnumerable<RequestSucursalHorarioDto> sucursalHorarios);
    }
}