using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces.Authorization;

public interface IServiceUserAuthorization
{
    /// <summary>
    /// Get logged user from context jwt
    /// </summary>
    /// <returns>ResponseUsuarioDto</returns>
    Task<ResponseUsuarioDto> GetLoggedUser();
}