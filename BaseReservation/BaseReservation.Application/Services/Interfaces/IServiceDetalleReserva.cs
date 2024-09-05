using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces
{
    public interface IServiceDetalleReserva
    {
        Task<ICollection<ResponseReservaServicioDto>> ListAllByReservaAsync(int idReserva);

        Task<ResponseReservaServicioDto?> FindByIdAsync(int id);

        Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<RequestReservaServicioDto> reservaServicios);
    }
}