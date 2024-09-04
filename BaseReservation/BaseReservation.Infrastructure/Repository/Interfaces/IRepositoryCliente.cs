using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCliente
{
    Task<ICollection<Cliente>> ListAllAsync();
}