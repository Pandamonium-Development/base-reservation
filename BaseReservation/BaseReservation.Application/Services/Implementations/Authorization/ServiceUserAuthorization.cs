using AutoMapper;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;

namespace BaseReservation.Application.Services.Implementations.Authorization;

public class ServiceUserAuthorization(IServiceUserContext serviceUserContext, IServiceUser serviceUser, IMapper mapper) : IServiceUserAuthorization
{
    /// <inheritdoc />
    public async Task<ResponseUserDto> GetLoggedUser()
    {
        var existingUser = await serviceUser.FindByEmailAsync(serviceUserContext.UserId!);
        var user = existingUser ?? throw new NotFoundException("No existe el usuario");
        return mapper.Map<ResponseUserDto>(user);
    }
}