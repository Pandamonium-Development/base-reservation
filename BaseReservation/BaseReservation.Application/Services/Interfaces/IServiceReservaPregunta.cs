using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceReservaPregunta
{
    /// <summary>
    /// Get list of all reservations questions
    /// </summary>
    /// <returns>ICollection of ResponseReservaPreguntaDto</returns>
    Task<ICollection<ResponseReservaPreguntaDto>> ListAllAsync();

    /// <summary>
    /// Get reservation question with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>ResponseReservaPreguntaDto</returns>
    Task<ResponseReservaPreguntaDto> FindByIdAsync(int id);
}