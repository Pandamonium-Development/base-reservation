using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceRole(IRepositoryRole repository, IMapper mapper) : IServiceRole
{
    /// <inheritdoc />
    public async Task<ResponseRoleDto> FindByIdAsync(byte id)
    {
        var rol = await repository.FindByIdAsync(id);
        if (rol == null) throw new NotFoundException("Rol no encontrado.");

        return mapper.Map<ResponseRoleDto>(rol);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseRoleDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseRoleDto>>(list);

        return collection;
    }
}