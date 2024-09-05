using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs.Authentication;

namespace BaseReservation.Application.Services.Interfaces;

public interface IServiceIdentity
{
    Task<TokenModel> LoginAsync(RequestUserLoginDto login);

    Task<TokenModel> RefreshTokenAsync(TokenModel request);
}
