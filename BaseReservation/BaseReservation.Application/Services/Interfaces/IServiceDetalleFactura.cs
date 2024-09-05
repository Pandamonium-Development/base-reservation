using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDetalleFactura
{
   Task<ResponseDetalleFacturaDto?> FindByIdAsync(long idFactura, long id);

   Task<ICollection<ResponseDetalleFacturaDto>> ListAllByFacturaAsync(long idFactura);
}
