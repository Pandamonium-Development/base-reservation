using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryFactura
{
    Task<Factura> CreateAsync(Factura factura, Pedido? pedido);

    Task<ICollection<Factura>> ListAllAsync();

    Task<Factura?> FindByIdAsync(long id);
}
