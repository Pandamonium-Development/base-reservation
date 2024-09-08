using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceRol(IRepositoryRol repository, IMapper mapper) : IServiceRol
{
    /// <inheritdoc />
    public async Task<ResponseRolDto> FindByIdAsync(byte id)
    {
        var rol = await repository.FindByIdAsync(id);
        if (rol == null) throw new NotFoundException("Rol no encontrado.");

        return mapper.Map<ResponseRolDto>(rol);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseRolDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseRolDto>>(list);

        return collection;
    }
}