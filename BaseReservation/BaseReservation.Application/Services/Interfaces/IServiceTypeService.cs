using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceTypeService
{
    /// <summary>
    /// Get list of all service's type
    /// </summary>
    /// <returns>ICollection of ResponseTypeServiceDto</returns>
    Task<ICollection<ResponseTypeServiceDto>> ListAllAsync();

    /// <summary>
    /// Get service's type with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseTypeServiceDto</returns>
    Task<ResponseTypeServiceDto> FindByIdAsync(byte id);
}