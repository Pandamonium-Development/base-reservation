using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReservaPregunta
{
    Task<ICollection<ResponseReservaPreguntaDto>> ListAllAsync();

    Task<ResponseReservaPreguntaDto> FindByIdAsync(int id);
}