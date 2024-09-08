using BaseReservation.Application.Configuration.Pagination;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProveedor
{
    /// <summary>
    /// Get list of all vendors
    /// </summary>
    /// <returns>ICollection of ResponseProveedorDto</returns>
    Task<ICollection<ResponseProveedorDto>> ListAllAsync();

    /// <summary>
    /// Get list of all vendors paginated
    /// </summary>
    /// <param name="paginationParameters">Pagination paramets options</param>
    /// <returns>PagedList of ResponseProveedorDto</returns>
    Task<PagedList<ResponseProveedorDto>> ListAllAsync(PaginationParameters paginationParameters);

    /// <summary>
    /// Get vendor with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseProveedorDto</returns>
    Task<ResponseProveedorDto> FindByIdAsync(byte id);

    /// <summary>
    /// Create vendor
    /// </summary>
    /// <param name="proveedorDto">Vendor request model to be added</param>
    /// <returns>ResponseProveedorDto</returns>
    Task<ResponseProveedorDto> CreateProveedorAsync(RequestProveedorDto proveedorDto);

    /// <summary>
    /// Update vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <param name="proveedorDto">Vendor request model to be updated</param>
    /// <returns>ResponseProveedorDto</returns>
    Task<ResponseProveedorDto> UpdateProveedorsync(byte id, RequestProveedorDto proveedorDto);

    /// <summary>
    /// Delete existing vendor
    /// </summary>
    /// <param name="id">Vendor id</param>
    /// <returns>bool</returns>
    Task<bool> DeleteProveedorsyncAsync(byte id);
}