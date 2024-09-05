using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceCategoria
{
    Task<ICollection<ResponseCategoriaDto>> ListAllAsync();

    Task<ResponseCategoriaDto> FindByIdAsync(byte id);
}
