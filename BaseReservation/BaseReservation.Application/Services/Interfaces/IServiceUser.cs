using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUser
{
    /// <summary>
    /// Get list of all users
    /// </summary>
    /// <param name="role">Role name can be specified to filter</param>
    /// <returns>ICollection of ResponseUserDto</returns>
    Task<ICollection<ResponseUserDto>> ListAllAsync(string? role = null);

    /// <summary>
    /// Get user with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUserDto</returns>
    Task<ResponseUserDto> FindByIdAsync(short id);

    /// <summary>
    /// Verify if user can be assigned to a branch
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="branchId">Branch id</param>
    /// <returns>True if is free, if not, false</returns>
    Task<bool> IsAvailableAsync(short id, byte branchId);
}