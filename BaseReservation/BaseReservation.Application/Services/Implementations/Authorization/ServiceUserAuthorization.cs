using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace ArtInk.Application.Services.Implementations.Authorization;

public class ServiceUserAuthorization(IServiceUserContext serviceUserContext, IRepositoryUsuario repositoryUsuario, IMapper mapper) : IServiceUserAuthorization
{
    public async Task<ResponseUsuarioDTO> GetLoggedUser()
    {
        var usuario = await repositoryUsuario.FindByEmailAsync(serviceUserContext.UserId!);
        var user = usuario ?? throw new NotFoundException("No existe el usuario");
        return mapper.Map<ResponseUsuarioDTO>(user);
    }
}