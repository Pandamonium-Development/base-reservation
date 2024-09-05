using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTipoServicio
{
    Task<ICollection<ResponseTipoServicioDto>> ListAsync();

    Task<ResponseTipoServicioDto> FindByIdAsync(byte id);
}