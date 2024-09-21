using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryHorario
{
    /// <summary>
    /// Creates a new Schedule
    /// </summary>
    /// <param name="horario">The Schedule entity to be added.</param>
    /// <returns>Horario</returns>
    Task<Horario> CreateHorarioAsync(Horario horario);

    /// <summary>
    /// Updates an existing Schedule.
    /// </summary>
    /// <param name="horario">The Shedule entity to update.</param>
    /// <returns>Horario</returns>
    Task<Horario> UpdateHorarioAsync(Horario horario);

    /// <summary>
    /// Lists all Schedules.
    /// </summary>
    /// <returns>ICollection of Horario</returns>
    Task<ICollection<Horario>> ListAllAsync();

    /// <summary>
    /// Finds a Schedule by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule.</param>
    /// <returns>Horario if founded, otherwise null</returns>
    Task<Horario?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if a Schedule with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsHorarioAsync(short id);

    /// <summary>
    /// Delete existing schedule
    /// </summary>
    /// <param name="id">Schedule id to look for</param>
    /// <returns>bool</returns>
    Task<bool> DeleteHorarioAsync(short id);
}