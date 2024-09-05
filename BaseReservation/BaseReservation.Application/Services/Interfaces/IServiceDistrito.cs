using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDistrito
{
    Task<ICollection<Distrito>> ListAllByCantonAsync(byte idCanton);

    Task<Distrito?> FindByIdAsync(byte id);
}
