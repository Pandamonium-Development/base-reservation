using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceFactura
{
    /// <summary>
    /// Creates a new Invoice 
    /// </summary>
    /// <param name="facturaDto">The data transfer object containing the information of the Factura to create</param>
    /// <returns>RequestFacturaDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseFacturaDto> CreateAsync(RequestFacturaDto facturaDto);

    /// <summary>
    /// Get list of all invoices
    /// </summary>
    /// <returns>ICollection of ResponseFacturaDto</returns>
    Task<ICollection<ResponseFacturaDto>> ListAllAsync();

    /// <summary>
    /// Finds a invoice by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the ResponseFacturaDto to retrieve.</param>
    /// <returns>ResponseFacturaDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseFacturaDto> FindByIdAsync(long id);
}