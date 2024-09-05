using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public  interface IServiceCanton
{
    Task<ICollection<ResponseCantonDto>> ListAllByProvinciaAsync(byte idProvincia);
    Task<ResponseCantonDto?> FindByIdAsync(byte id);
}
