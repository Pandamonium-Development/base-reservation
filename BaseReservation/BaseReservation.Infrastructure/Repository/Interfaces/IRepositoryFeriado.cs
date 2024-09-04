using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryFeriado
{
    Task<ICollection<Feriado>> ListAllAsync();

    Task<Feriado?> FindByIdAsync(byte id);

    Task<Feriado> CreateFeriadoAsync(Feriado feriado);

    Task<Feriado> UpdateFeriadoAsync(Feriado feriado);

    Task<bool> ExistsFeriadoAsync(byte id);

    Task<bool> DeleteFeriadoAsync(byte id);
}