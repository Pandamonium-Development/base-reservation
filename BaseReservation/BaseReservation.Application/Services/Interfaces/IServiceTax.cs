using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTax
{
    /// <summary>
    /// Get list of all taxes
    /// </summary>
    /// <returns>ICollection of ResponseTaxDto</returns>
    Task<ICollection<ResponseTaxDto>> ListAllAsync();
}