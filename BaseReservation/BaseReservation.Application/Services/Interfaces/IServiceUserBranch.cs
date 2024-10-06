using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUserBranch
{
    /// <summary>
    /// Create branch's users 
    /// </summary>
    /// <param name="branchId">Branch id that receives list of users</param>
    /// <param name="branchUsers">List of branch's users</param>
    /// <returns>bool</returns>
    Task<bool> CreateUserBranchAsync(byte branchId, IEnumerable<RequestUserBranchDto> branchUsers);
}