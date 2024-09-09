using BaseReservation.Application.ResponseDTOs;
namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProvincia
{
    /// <summary>
    /// Get list of all provinces
    /// </summary>
    /// <returns>ICollection of ResponseProvinciaDto</returns>
    Task<ICollection<ResponseProvinciaDto>> ListAllAsync();

    /// <summary>
    /// Get province with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProvinciaDto</returns>
    Task<ResponseProvinciaDto> FindByIdAsync(byte id);
}