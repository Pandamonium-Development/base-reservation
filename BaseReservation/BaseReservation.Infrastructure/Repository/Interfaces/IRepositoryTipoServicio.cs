using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTipoServicio
{
    Task<ICollection<TipoServicio>> ListAllAsync();

    Task<TipoServicio?> FindByIdAsync(byte id);
}
