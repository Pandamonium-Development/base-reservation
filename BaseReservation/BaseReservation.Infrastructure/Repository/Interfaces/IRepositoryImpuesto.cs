using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryImpuesto
{
    Task<ICollection<Impuesto>> ListAllAsync();
}