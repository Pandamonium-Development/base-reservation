using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTipoPago
{
    Task<ICollection<TipoPago>> ListAllAsync();
}