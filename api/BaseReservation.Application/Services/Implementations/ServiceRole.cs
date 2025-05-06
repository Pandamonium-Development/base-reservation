using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.Dtos.Response;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceRole(ICoreService<Role> coreService, IMapper mapper) : IServiceRole
{
    /// <inheritdoc />
    public async Task<ResponseRoleDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<Role>().ExistsAsync(id)) throw new NotFoundException("Rol no encontrado.");

        var spec = new BaseSpecification<Role>(x => x.Id == id);
        var role = await coreService.UnitOfWork.Repository<Role>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseRoleDto>(role);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseRoleDto>> ListAllAsync()
    {
        var roles = await coreService.UnitOfWork.Repository<Role>().ListAllAsync();
        return mapper.Map<ICollection<ResponseRoleDto>>(roles);
    }
}