using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursal
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of ResponseSucursalDto</returns>
    Task<ICollection<ResponseSucursalDto>> ListAllAsync();

    /// <summary>
    /// Get list of all branches by role that has user logged in
    /// </summary>
    /// <returns>ICollection of ResponseSucursalDto</returns>
    Task<ICollection<ResponseSucursalDto>> ListAllByRolAsync();

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> FindByIdAsync(byte id);

    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="sucursalDTO">Request branch model to be addded</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> CreateSucursalAsync(RequestSucursalDto sucursalDTO);

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="id">Id to identify record</param>
    /// <param name="sucursalDTO">Request branch model to be updated</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> UpdateSucursalAsync(byte id, RequestSucursalDto sucursalDTO);
}