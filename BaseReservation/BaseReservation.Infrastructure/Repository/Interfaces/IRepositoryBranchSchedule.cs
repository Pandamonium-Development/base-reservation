using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryBranchSchedule
{
    /// <summary>
    /// Get list of all branch schedules by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of BranchSchedule</returns>
    Task<ICollection<BranchSchedule>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Get branch schedule by id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>BranchSchedule if found, otherwise null</returns>
    Task<BranchSchedule?> FindByIdAsync(short id);

    /// <summary>
    /// Get branch schedule by specific day of week
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="dia">Day of week</param>
    /// <returns>BranchSchedule if found, otherwise null</returns>
    Task<BranchSchedule?> FindByWeekDayAsync(byte branchId, WeekDay day);

    /// <summary>
    /// Create multiple branch schedule
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branchSchedules">List of branch schedules</param>
    /// <returns>True if all were added, if not, false</returns>
    Task<bool> CreateBranchSchedulesAsync(byte branchId, IEnumerable<BranchSchedule> branchSchedules);
}