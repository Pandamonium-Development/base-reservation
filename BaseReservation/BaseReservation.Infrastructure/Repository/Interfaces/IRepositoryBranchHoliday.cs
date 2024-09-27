
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryBranchHoliday
{
    /// <summary>
    /// Get list of all branch holidays by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of BranchHoliday</returns>
    Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Get list of all branch holidays by branch, start date and end date
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="startDate">Start date to filter</param>
    /// <param name="endDate">End date to filter</param>
    /// <returns>ICollection of BranchHoliday</returns>
    Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId, DateOnly startDate, DateOnly endDate);

    /// <summary>
    /// Get list of all branch holidays by branch and year
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="year">Year to look for</param>
    /// <returns>ICollection of BranchHoliday</returns>
    Task<ICollection<BranchHoliday>> ListAllByBranchAsync(byte branchId, short year);

    /// <summary>
    /// Get list of branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>BranchHoliday if founded, otherwise null</returns>
    Task<BranchHoliday?> FindByIdAsync(short id);

    /// <summary>
    /// Create branch holidays
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="branchHolidays">List of branch holidays to be added</param>
    /// <returns>True if all were added, if not, false</returns>
    Task<bool> CreateBranchHolidaysAsync(byte branchId, IEnumerable<BranchHoliday> branchHolidays);
}