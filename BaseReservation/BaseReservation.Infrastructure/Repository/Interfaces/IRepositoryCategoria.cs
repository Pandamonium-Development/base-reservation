using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCategoria
{
    Task<ICollection<Categoria>> ListAllAsync();

    Task<Categoria?> FindByIdAsync(byte id);
}