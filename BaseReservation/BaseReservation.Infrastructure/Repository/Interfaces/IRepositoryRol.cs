using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryRol
{
    Task<ICollection<Rol>> ListAllAsync();

    Task<Rol?> FindByIdAsync(byte id);
}
