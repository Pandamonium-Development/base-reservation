using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;
public interface IServiceCliente
{
    /// <summary>
    /// Retrieves a list of all clientes
    /// </summary>
    /// <returns>ICollection ResponseClienteDto</returns>
    Task<ICollection<ResponseClienteDto>> ListAllAsync();
}
