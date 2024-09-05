using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetalleReserva
{
    Task<ICollection<ResponseDetalleReservaDto>> ListAllByReservaAsync(int idReserva);

    Task<ResponseDetalleReservaDto?> FindByIdAsync(int id);

    Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<RequestDetalleReservaDto> detallesReserva);
}