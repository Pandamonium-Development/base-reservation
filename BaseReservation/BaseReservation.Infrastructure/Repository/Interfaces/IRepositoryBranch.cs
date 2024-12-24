using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryBranch
{
    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="branch">Branch model to be added</param>
    /// <returns>Branch</returns>
    Task<Branch> CreateBranchAsync(Branch branch);

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="branch">Branch model to be updated</param>
    /// <returns>Branch</returns>
    Task<Branch> UpdateBranchAsync(Branch branch);

    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of Branch</returns>
    Task<ICollection<Branch>> ListAllAsync();

    /// <summary>
    /// Get list of all branches by role
    /// </summary>
    /// <param name="rol">Role to look for</param>
    /// <returns>ICollection of Branch</returns>
    Task<ICollection<Branch>> ListAllByRoleAsync(string rol);

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Branch if founded, otherwise null</returns>
    Task<Branch?> FindByIdAsync(byte id);

    /// <summary>
    /// Validate if exists branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsBranchAsync(byte id);

    /// <summary>
    /// Deletes a branch based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the branch to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteBranchAsync(byte id);
}