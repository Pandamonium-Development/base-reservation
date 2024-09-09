using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceRol
{
    /// <summary>
    /// Get list of all roles
    /// </summary>
    /// <returns>ICollection of ResponseRolDto</returns>
    Task<ICollection<ResponseRolDto>> ListAllAsync();

    /// <summary>
    /// Get role with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseRolDto</returns>
    Task<ResponseRolDto> FindByIdAsync(byte id);
}