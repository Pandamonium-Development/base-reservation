using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryBranchScheduleBlock
{
    /// <summary>
    /// Create a branch schedule block
    /// </summary>
    /// <param name="branchScheduleBlock">Branch schedule block model to be added</param>
    /// <returns>BranchScheduleBlock</returns>
    Task<BranchScheduleBlock> CreateBranchScheduleBlockAsync(BranchScheduleBlock branchScheduleBlock);

    /// <summary>
    /// Create multiple schedule branch blocks
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <param name="branchScheduleBlocks">List of schedule branch blocks to be added</param>
    /// <returns>True if all items were saved, if not, false</returns>
    Task<bool> CreateBranchScheduleBlockAsync(short branchScheduleId, IEnumerable<BranchScheduleBlock> branchScheduleBlocks);

    /// <summary>
    /// Update branch schedule block
    /// </summary>
    /// <param name="branchScheduleBlock">Branch schedule block model to be added</param>
    /// <returns>BranchScheduleBlock</returns>
    Task<BranchScheduleBlock> UpdateBranchScheduleBlockAsync(BranchScheduleBlock branchScheduleBlock);

    /// <summary>
    /// Get list of all branch schedule blocks by branch schedule
    /// </summary>
    /// <param name="branchScheduleId">Branch schedule id</param>
    /// <returns>ICollection of BranchScheduleBlock</returns>
    Task<ICollection<BranchScheduleBlock>> ListAllByBranchScheduleAsync(short branchScheduleId);

    /// <summary>
    /// Get list of all branch schedule blocks by branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <returns>ICollection of BranchScheduleBlock</returns>
    Task<ICollection<BranchScheduleBlock>> ListAllByBranchAsync(byte branchId);

    /// <summary>
    /// Get branch schedule block with specific id
    /// </summary>
    /// <param name="id">Branch schedule block id</param>
    /// <returns>BranchScheduleBlock</returns>
    Task<BranchScheduleBlock?> FindByIdAsync(long id);

    /// <summary>
    /// Validate if exists branch schedule block
    /// </summary>
    /// <param name="id">Branch schedule block Id</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsBranchScheduleBlockAsync(long id);
}