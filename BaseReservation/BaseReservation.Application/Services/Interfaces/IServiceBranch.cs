using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceBranch
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of ResponseBranchDto</returns>
    Task<ICollection<ResponseBranchDto>> ListAllAsync();

    /// <summary>
    /// Get list of all branches by role that has user logged in
    /// </summary>
    /// <returns>ICollection of ResponseBranchDto</returns>
    Task<ICollection<ResponseBranchDto>> ListAllByRoleAsync();

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseBranchDto</returns>
    Task<ResponseBranchDto> FindByIdAsync(byte id);

    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="branchDTO">Request branch model to be addded</param>
    /// <returns>ResponseBranchDto</returns>
    Task<ResponseBranchDto> CreateBranchAsync(RequestBranchDto branchDTO);

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="id">Id to identify record</param>
    /// <param name="branchDTO">Request branch model to be updated</param>
    /// <returns>ResponseBranchDto</returns>
    Task<ResponseBranchDto> UpdateBranchAsync(byte id, RequestBranchDto branchDTO);

    /// <summary>
    /// Deletes a branch based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the branch to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteBranchAsync(byte id);
}