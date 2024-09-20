using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceSucursal
{
    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of ResponseSucursalDto</returns>
    Task<ICollection<ResponseSucursalDto>> ListAllAsync();

    /// <summary>
    /// Get list of all branches by role that has user logged in
    /// </summary>
    /// <returns>ICollection of ResponseSucursalDto</returns>
    Task<ICollection<ResponseSucursalDto>> ListAllByRolAsync();

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> FindByIdAsync(byte id);

    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="branchDTO">Request branch model to be addded</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> CreateBranchAsync(RequestSucursalDto branchDTO);

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="id">Id to identify record</param>
    /// <param name="branchDTO">Request branch model to be updated</param>
    /// <returns>ResponseSucursalDto</returns>
    Task<ResponseSucursalDto> UpdateBranchAsync(byte id, RequestSucursalDto branchDTO);

    /// <summary>
    /// Deletes a branch based on the provided Id.
    /// </summary>
    /// <param name="id">Id of the branch to delete.</param>
    /// <returns>True if successful, otherwise false.</returns>
    Task<bool> DeleteBranchAsync(byte id);
}