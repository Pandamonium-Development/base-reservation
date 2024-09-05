using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUnidadMedida
{
    Task<ICollection<ResponseUnidadMedidaDto>> ListAllAsync();

    Task<ResponseUnidadMedidaDto> FindByIdAsync(byte id);
}