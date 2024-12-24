using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceRole
{
    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>ICollection of ResponseRoleDto</returns>
    Task<ICollection<ResponseRoleDto>> ListAllAsync();

    /// <summary>
    /// Get role with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseRoleDto</returns>
    Task<ResponseRoleDto> FindByIdAsync(byte id);
}