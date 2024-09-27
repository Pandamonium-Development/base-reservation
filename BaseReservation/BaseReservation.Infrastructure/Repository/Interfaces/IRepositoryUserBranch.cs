using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryUserBranch
{
    /// <summary>
    /// Assign users to a branch
    /// </summary>
    /// <param name="branchId">Branch id</param>
    /// <param name="usersBranch">List of users to be assign</param>
    /// <returns>True if all users were added correctly, if not, false</returns>
    Task<bool> AssignUsersAsync(byte branchId, IEnumerable<UserBranch> usersBranch);

    /// <summary>
    /// Get list of all user by a branch
    /// </summary>
    /// <param name="branchId">Branch id to filter</param>
    /// <returns>ICollection of UserBranch</returns>
    Task<ICollection<UserBranch>> ListAllByBranchAsync(byte branchId);
}