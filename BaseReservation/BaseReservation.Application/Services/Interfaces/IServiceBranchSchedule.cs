using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces
{
    public interface IServiceBranchSchedule
    {
        /// <summary>
        /// Get list of all branch's schedules by branch
        /// </summary>
        /// <param name="branchId">Branch id</param>
        /// <returns>ICollection of ResponseBranchScheduleDto</returns>
        Task<ICollection<ResponseBranchScheduleDto>> ListAllByBranchAsync(byte branchId);

        /// <summary>
        /// Get branch schedule with specific id
        /// </summary>
        /// <param name="id">Branch scheduel id to look for</param>
        /// <returns>ResponseBranchScheduleDto</returns>
        Task<ResponseBranchScheduleDto?> FindByIdAsync(short id);

        /// <summary>
        /// Create branch's schedules
        /// </summary>
        /// <param name="branchId">Branch id that receive schedules</param>
        /// <param name="branchSchedules">List of Branch's schedules will be added</param>
        /// <returns>bool</returns>
        Task<bool> CreateBranchScheduleAsync(byte branchId, IEnumerable<RequestBranchScheduleDto> branchSchedules);
    }
}