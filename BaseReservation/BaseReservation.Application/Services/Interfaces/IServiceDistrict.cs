using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceDistrict
{
    /// <summary>
    /// Get a list of all districts.
    /// </summary>
    /// <param name="cantonId">The identifier of the ResponseDistrictDto</param>
    /// <returns>ICollection of ResponseDistrictDto</returns>
    Task<ICollection<ResponseDistrictDto>> ListAllByCantonAsync(byte cantonId);

    /// <summary>
    ///  Finds a district by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the ResponseDistrictDto to retrieve.</param>
    /// <returns>ResponseDistrictDto</returns>
    /// <exception cref="NotFoundException"></exception>
    Task<ResponseDistrictDto> FindByIdAsync(byte id);
}