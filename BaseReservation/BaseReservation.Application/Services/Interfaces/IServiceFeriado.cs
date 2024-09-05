using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceFeriado
{
    Task<ICollection<ResponseFeriadoDto>> ListAllAsync();

    Task<ResponseFeriadoDto?> FindByIdAsync(byte id);

    Task<ResponseFeriadoDto> CreateFeriadoAsync(ResponseFeriadoDto feriadoDto);

    Task<ResponseFeriadoDto> UpdateFeriadoAsync(ResponseFeriadoDto feriadoDto);

    Task<bool> ExistsFeriadoAsync(byte id);

    Task<bool> DeleteFeriadoAsync(byte id);
}
