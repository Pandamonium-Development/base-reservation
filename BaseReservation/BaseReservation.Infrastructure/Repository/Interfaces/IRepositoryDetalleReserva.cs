using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces
{
    public interface IRepositoryDetalleReserva
    {
        Task<ICollection<DetalleReserva>> ListAllByReservaAsync(int idReserva);

        Task<DetalleReserva?> FindByIdAsync(int id);

        Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<DetalleReserva> detallesReserva);
    }
}
