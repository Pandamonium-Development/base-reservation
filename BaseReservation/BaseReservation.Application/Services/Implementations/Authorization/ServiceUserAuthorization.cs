using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations.Authorization;

public class ServiceUserAuthorization(IServiceUserContext serviceUserContext, IRepositoryUser repositoryUser, IMapper mapper) : IServiceUserAuthorization
{
    /// <inheritdoc />
    public async Task<ResponseUserDto> GetLoggedUser()
    {
        var existingUser = await repositoryUser.FindByEmailAsync(serviceUserContext.UserId!);
        var user = existingUser ?? throw new NotFoundException("No existe el usuario");
        return mapper.Map<ResponseUserDto>(user);
    }
}