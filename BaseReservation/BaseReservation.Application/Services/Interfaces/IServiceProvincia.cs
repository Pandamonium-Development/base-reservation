using BaseReservation.Application.ResponseDTOs;
namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProvincia
{
    Task<ICollection<ResponseProvinciaDto>> ListAllAsync();

    Task<ResponseProvinciaDto> FindByIdAsync(byte id);
}