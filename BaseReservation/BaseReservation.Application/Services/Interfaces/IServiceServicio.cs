using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceServicio
{
    /// <summary>
    /// Get list of all services
    /// </summary>
    /// <returns>ICollection of ResponseServicioDto</returns>
    Task<ICollection<ResponseServicioDto>> ListAllAsync();

    /// <summary>
    /// Get Service with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseServicioDto</returns>
    Task<ResponseServicioDto> FindByIdAsync(byte id);

    /// <summary>
    /// Create a service
    /// </summary>
    /// <param name="servicio">Request servide model to be added</param>
    /// <returns>ResponseServicioDto</returns>
    Task<ResponseServicioDto> CreateServicioAsync(RequestServicioDto servicio);

    /// <summary>
    /// Update a service
    /// </summary>
    /// <param name="id">Id to identify record</param>
    /// <param name="servicioDTO">Request service model to be updated</param>
    /// <returns>ResponseServicioDto</returns>
    Task<ResponseServicioDto> UpdateServicioAsync(byte id, RequestServicioDto servicioDTO);
}