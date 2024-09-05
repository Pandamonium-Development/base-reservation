using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceRol
{
    Task<ICollection<ResponseRolDto>> ListAsync();

    Task<ResponseRolDto> FindByIdAsync(byte id);
}