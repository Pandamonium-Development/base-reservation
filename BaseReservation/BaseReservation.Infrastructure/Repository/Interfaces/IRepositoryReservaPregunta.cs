using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReservaPregunta
{
    Task<ICollection<ReservaPregunta>> ListAllAsync();

    Task<ReservaPregunta?> FindByIdAsync(int id);
}
