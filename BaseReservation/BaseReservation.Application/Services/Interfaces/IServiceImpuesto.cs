using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceImpuesto
{
    /// <summary>
    /// Get list of all taxes
    /// </summary>
    /// <returns>ICollection of ResponseImpuestoDto</returns>
    Task<ICollection<ResponseImpuestoDto>> ListAllAsync();
}