using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTipoPago
{
    /// <summary>
    /// Get list of all payment types
    /// </summary>
    /// <returns>ICollection of ResponseTipoPagoDto</returns>
    Task<ICollection<ResponseTipoPagoDto>> ListAllAsync();
}