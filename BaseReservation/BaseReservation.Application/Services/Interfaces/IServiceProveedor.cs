using BaseReservation.Application.Configuration.Pagination;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceProveedor
{
    Task<ICollection<ResponseProveedorDto>> ListAllAsync();

    Task<PagedList<ResponseProveedorDto>> ListAllAsync(PaginationParameters paginationParameters);

    Task<ResponseProveedorDto> FindByIdAsync(byte id);

    Task<ResponseProveedorDto> CreateProveedorAsync(RequestProveedorDto proveedorDto);

    Task<ResponseProveedorDto> UpdateProveedorsync(byte id, RequestProveedorDto proveedorDto);

    Task<bool> DeleteProveedorsyncAsync(byte id);
}