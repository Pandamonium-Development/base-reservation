using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceUserBranch(IRepositoryUserBranch repository, IMapper mapper, IValidator<UserBranch> usuarioSucursalValidator) : IServiceUserBranch
{
    /// <inheritdoc />
    public async Task<bool> CreateUserBranchAsync(byte branchId, IEnumerable<RequestUserBranchDto> usersBranchDto)
    {
        var usersBranch = await ValidateUsuariosSucursalAsync(branchId, usersBranchDto);
        return await repository.AssignUsersAsync(branchId, usersBranch);
    }

    /// <summary>
    /// Validate Branch's users to be added
    /// </summary>
    /// <param name="branchId">Branch id that receive users</param>
    /// <param name="usersBranch">List of branch's users to be added</param>
    /// <returns>IEnumerable of UserBranch</returns>
    private async Task<IEnumerable<UserBranch>> ValidateUsuariosSucursalAsync(byte branchId, IEnumerable<RequestUserBranchDto> usersBranch)
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