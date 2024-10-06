using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServicePaymentType
{
    /// <summary>
    /// Get list of all payment types
    /// </summary>
    /// <returns>ICollection of ResponsePaymentTypeDto</returns>
    Task<ICollection<ResponsePaymentTypeDto>> ListAllAsync();
}