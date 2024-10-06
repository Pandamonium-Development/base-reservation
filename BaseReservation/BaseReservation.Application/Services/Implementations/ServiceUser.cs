using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.ResponseDTOs.Enums;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUser(IRepositoryUser repository, IRepositoryBranch repositoryBranch, IMapper mapper) : IServiceUser
{
    /// <inheritdoc />
    public async Task<ResponseUserDto> FindByIdAsync(short id)
    {
        var user = await repository.FindByIdAsync(id);
        if (user == null) throw new NotFoundException("Usuario no encontrado.");

        return mapper.Map<ResponseUserDto>(user);
    }

    /// <inheritdoc />
    public async Task<bool> IsAvailableAsync(short id, byte branchId)
    {
        var user = await repository.ExistsUserAsync(id);
        if (!user) throw new NotFoundException("Usuario no encontrado.");

        var branch = await repositoryBranch.ExistsBranchAsync(branchId);
        if (!branch) throw new NotFoundException("Sucursal no encontrada.");

        return await repository.IsAvailableAsync(id, branchId);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseUserDto>> ListAllAsync(string? role = null)
    {
        if (role == null)
        {
            var list = await repository.ListAllAsync();
            return mapper.Map<ICollection<ResponseUserDto>>(list);
        }

        Role roleEnum;
        if (!Enum.TryParse(role, out roleEnum)) throw new BaseReservationException("Rol Inválido");

        var listFilter = await repository.ListAllByRoleAsync((byte)roleEnum);
        var collection = mapper.Map<ICollection<ResponseUserDto>>(listFilter);

        return collection;
    }
}