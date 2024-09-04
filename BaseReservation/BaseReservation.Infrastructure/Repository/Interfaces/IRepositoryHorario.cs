using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryHorario
{
    Task<Horario> CreateHorarioAsync(Horario horario);

    Task<Horario> UpdateHorarioAsync(Horario horario);

    Task<ICollection<Horario>> ListAllAsync();

    Task<Horario?> FindByIdAsync(short id);

    Task<bool> ExistsHorarioAsync(short id);
}