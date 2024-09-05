using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUnidadMedida
{
    Task<ICollection<ResponseUnidadMedidaDto>> ListAsync();

    Task<ResponseUnidadMedidaDto> FindByIdAsync(byte id);
}