using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceRol
{
    Task<ICollection<ResponseRolDto>> ListAllAsync();

    Task<ResponseRolDto> FindByIdAsync(byte id);
}