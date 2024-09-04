using BaseReservation.Application.ResponseDTOs;

namespace BaseReservation.Application.Services.Interfaces.Authorization;

public interface IServiceUserAuthorization
{
    Task<ResponseUsuarioDTO> GetLoggedUser();
}