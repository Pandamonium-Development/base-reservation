using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetalleFactura
{
    /// <summary>
    ///  Finds a DetalleFactura by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the DetalleFactura to retrieve.</param>
    /// <returns>ResponseDetalleFacturaDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseDetalleFacturaDto> FindByIdAsync(long id);

    /// <summary>
    /// Retrieves a list of all Facturas.
    /// </summary>
    /// <param name="idFactura">The identifier of the Factura</param>
    /// <returns>ICollection of ResponseDetalleFacturaDto</returns>
    Task<ICollection<ResponseDetalleFacturaDto>> ListAllByFacturaAsync(long idFactura);
}
