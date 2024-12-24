using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceBranchHoliday
{
    /// <summary>
    /// Get list of all holiday's branch by year
    /// </summary>
    /// <param name="branchId">Branch id to look for</param>
    /// <param name="year">Year to look for/param>
    /// <returns>ICollection of ResponseBranchHolidayDto</returns>
    Task<ICollection<ResponseBranchHolidayDto>> ListAllByBranchAsync(byte branchId, short? year);

    /// <summary>
    /// Get Branch holiday with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseBranchHolidayDto</returns>
    Task<ResponseBranchHolidayDto> FindByIdAsync(short id);

    /// <summary>
    /// Create branch's holidays
    /// </summary>
    /// <param name="branchId">Branch id to receive holidays</param>
    /// <param name="branchHolidays">List of branch holiday request mode to be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateBranchHolidaysAsync(byte branchId, IEnumerable<RequestBranchHolidayDto> branchHolidays);
}