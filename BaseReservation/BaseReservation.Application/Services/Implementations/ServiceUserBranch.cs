using AutoMapper;
using FluentValidation;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUserBranch(ICoreService<UserBranch> coreService, IServiceUser serviceUser, IServiceBranch serviceBranch,
                                IMapper mapper, IValidator<UserBranch> usuarioSucursalValidator) : IServiceUserBranch
{
    /// <inheritdoc />
    public async Task<bool> CreateUserBranchAsync(long branchId, IEnumerable<RequestUserBranchDto> usersBranchDto)
    {
        var usersBranch = await ValidateUsuariosSucursalAsync(branchId, usersBranchDto);

        var listSaved = await coreService.UnitOfWork.Repository<UserBranch>().AddRangeAsync(usersBranch.ToList());
        await coreService.UnitOfWork.SaveChangesAsync();

        return listSaved.Any();
    }

    /// <inheritdoc />
    public async Task<bool> IsAvailableAsync(long userId, long branchId)
    {
        var user = await serviceUser.ExistsUserAsync(userId);
        if (!user) throw new NotFoundException("Usuario no encontrado.");

        var branch = await serviceBranch.ExistsBranchAsync(branchId);
        if (!branch) throw new NotFoundException("Sucursal no encontrada.");

        var spec = new BaseSpecification<UserBranch>(x => x.UserId == userId && x.BranchId != branchId);
        return await coreService.UnitOfWork.Repository<UserBranch>().CountAsync(spec) != 0;
    }

    /// <summary>
    /// Validate Branch's users to be added
    /// </summary>
    /// <param name="branchId">Branch id that receive users</param>
    /// <param name="usersBranch">List of branch's users to be added</param>
    /// <returns>IEnumerable of UserBranch</returns>
    private async Task<IEnumerable<UserBranch>> ValidateUsuariosSucursalAsync(long branchId, IEnumerable<RequestUserBranchDto> usersBranch)
    {
        var existingUsersBranch = mapper.Map<List<UserBranch>>(usersBranch);
        foreach (var item in existingUsersBranch)
        {
            item.BranchId = branchId;
            await usuarioSucursalValidator.ValidateAndThrowAsync(item);
        }
        return existingUsersBranch;
    }
}