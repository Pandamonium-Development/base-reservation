using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceServicio
{
    Task<ICollection<ResponseServicioDto>> ListAsync();

    Task<ResponseServicioDto> FindByIdAsync(byte id);

    Task<ResponseServicioDto> CreateServicioAsync(RequestServicioDto servicio);

    Task<ResponseServicioDto> UpdateServicioAsync(byte id, RequestServicioDto servicioDTO);
}