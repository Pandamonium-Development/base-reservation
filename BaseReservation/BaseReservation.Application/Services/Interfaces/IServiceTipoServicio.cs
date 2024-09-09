using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTipoServicio
{
    /// <summary>
    /// Get list of all service's type
    /// </summary>
    /// <returns>ICollection of ResponseTipoServicioDto</returns>
    Task<ICollection<ResponseTipoServicioDto>> ListAllAsync();

    /// <summary>
    /// Get service's type with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseTipoServicioDto</returns>
    Task<ResponseTipoServicioDto> FindByIdAsync(byte id);
}