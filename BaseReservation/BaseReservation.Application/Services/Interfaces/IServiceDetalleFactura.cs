using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetalleFactura
{
    /// <summary>
    ///  Finds a invoice detail by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the invoice detail to retrieve.</param>
    /// <returns>ResponseDetalleFacturaDto</returns>
    Task<ResponseDetalleFacturaDto> FindByIdAsync(long id);

    /// <summary>
    /// Retrieves a list of all details by invoice.
    /// </summary>
    /// <param name="idFactura">The identifier of the invoice</param>
    /// <returns>ICollection of ResponseDetalleFacturaDto</returns>
    Task<ICollection<ResponseDetalleFacturaDto>> ListAllByFacturaAsync(long idFactura);
}