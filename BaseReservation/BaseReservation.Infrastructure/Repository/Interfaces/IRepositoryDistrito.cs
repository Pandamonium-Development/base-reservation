using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryDistrito
{
    Task<ICollection<Distrito>> ListAllByCantonAsync(byte idCanton);

    Task<Distrito?> FindByIdAsync(byte id);
}