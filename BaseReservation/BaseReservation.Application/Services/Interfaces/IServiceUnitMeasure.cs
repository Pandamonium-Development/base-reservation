using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUnitMeasure
{
    /// <summary>
    /// Get list of all units of measurement
    /// </summary>
    /// <returns>ICollection of ResponseUnitMeasureDto</returns>
    Task<ICollection<ResponseUnitMeasureDto>> ListAllAsync();

    /// <summary>
    /// Get unit of measurement with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUnitMeasureDto</returns>
    Task<ResponseUnitMeasureDto> FindByIdAsync(byte id);
}