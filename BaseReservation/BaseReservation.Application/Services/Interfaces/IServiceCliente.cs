using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;
public interface IServiceCliente
{
    Task<ICollection<ResponseClienteDto>> ListAllAsync();
}
