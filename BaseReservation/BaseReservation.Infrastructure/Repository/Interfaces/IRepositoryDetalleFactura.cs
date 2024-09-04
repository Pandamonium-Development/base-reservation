using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDetalleFactura
{
    Task<DetalleFactura?> FindByIdAsync(long id);

    Task<ICollection<DetalleFactura>> ListAllByFacturaAsync(long idFactura);
}
