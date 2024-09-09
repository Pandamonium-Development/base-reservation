using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceCliente
{
    /// <summary>
    /// Get a list of all customers
    /// </summary>
    /// <returns>ICollection ResponseClienteDto</returns>
    Task<ICollection<ResponseClienteDto>> ListAllAsync();
}