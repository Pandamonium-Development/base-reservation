using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations.Authorization;

public class ServiceUserAuthorization(IServiceUserContext serviceUserContext, IRepositoryUsuario repositoryUsuario, IMapper mapper) : IServiceUserAuthorization
{
    /// <inheritdoc />
    public async Task<ResponseUsuarioDto> GetLoggedUser()
    {
        var usuario = await repositoryUsuario.FindByEmailAsync(serviceUserContext.UserId!);
        var user = usuario ?? throw new NotFoundException("No existe el usuario");
        return mapper.Map<ResponseUsuarioDto>(user);
    }
}