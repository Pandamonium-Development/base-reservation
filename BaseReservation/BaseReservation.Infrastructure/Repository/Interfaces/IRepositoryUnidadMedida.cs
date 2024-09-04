using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUnidadMedida
{
    Task<ICollection<UnidadMedida>> ListAllAsync();

    Task<UnidadMedida?> FindByIdAsync(byte id);
}
