using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySchedule
{
    /// <summary>
    /// Creates a new Schedule
    /// </summary>
    /// <param name="schedule">The Schedule entity to be added.</param>
    /// <returns>Schedule</returns>
    Task<Schedule> CreateScheduleAsync(Schedule schedule);

    /// <summary>
    /// Updates an existing Schedule.
    /// </summary>
    /// <param name="schedule">The Shedule entity to update.</param>
    /// <returns>Schedule</returns>
    Task<Schedule> UpdateScheduleAsync(Schedule schedule);

    /// <summary>
    /// Lists all Schedules.
    /// </summary>
    /// <returns>ICollection of Schedule</returns>
    Task<ICollection<Schedule>> ListAllAsync();

    /// <summary>
    /// Finds a Schedule by its unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule.</param>
    /// <returns>Schedule if founded, otherwise null</returns>
    Task<Schedule?> FindByIdAsync(short id);

    /// <summary>
    /// Checks if a Schedule with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Schedule</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsScheduleAsync(short id);

    /// <summary>
    /// Delete existing schedule
    /// </summary>
    /// <param name="id">Schedule id to look for</param>
    /// <returns>bool</returns>
    Task<bool> DeleteScheduleAsync(short id);
}