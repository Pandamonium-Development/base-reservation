using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceBranchScheduleBlock
{
    /// <summary>
    /// Get list of all blocks by branch schedule
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <returns>ICollection of ResponseBranchScheduleBlockDto</returns>
    Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchScheduleAsync(short branchScheduleId);

    /// <summary>
    /// Get Branch schedule block with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> FindByIdAsync(long id);

    /// <summary>
    /// Create branch schedule block
    /// </summary>
    /// <param name="branchScheduleBlock">Branch schedule block request model to be added</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> CreateBranchScheduleBlockAsync(RequestBranchScheduleBlockDto branchScheduleBlock);

    /// <summary>
    /// Create branch schedule's blocks
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id that receive blocks</param>
    /// <param name="branchScheduleBlocks">List of Branch schedule's blocks will be added</param>
    /// <returns>bool</returns>
    Task<bool> CreateBranchScheduleBlockAsync(short branchScheduleId, IEnumerable<RequestBranchScheduleBlockDto> branchScheduleBlocks);

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block id to identiy the record</param>
    /// <param name="branchScheduleBlock">Branch schedule block request model to be updated</param>
    /// <returns>ResponseBranchScheduleBlockDto</returns>
    Task<ResponseBranchScheduleBlockDto> UpdateBranchScheduleBlockAsync(long id, RequestBranchScheduleBlockDto branchScheduleBlock);
}