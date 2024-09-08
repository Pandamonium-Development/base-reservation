using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceUnidadMedida
{
    /// <summary>
    /// Get list of all units of measurement
    /// </summary>
    /// <returns>ICollection of ResponseUnidadMedidaDto</returns>
    Task<ICollection<ResponseUnidadMedidaDto>> ListAllAsync();

    /// <summary>
    /// Get unit of measurement with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseUnidadMedidaDto</returns>
    Task<ResponseUnidadMedidaDto> FindByIdAsync(byte id);
}