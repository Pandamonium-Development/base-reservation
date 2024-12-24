using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceBranch(IRepositoryBranch repository, IMapper mapper,
                            IValidator<Branch> branchValidator,
                            IServiceUserAuthorization serviceUserAuthorization) : IServiceBranch
{
    /// <inheritdoc />
    public async Task<ResponseBranchDto> CreateBranchAsync(RequestBranchDto branchDTO)
    {
        var branch = await ValidateBranch(branchDTO);

        var result = await repository.CreateBranchAsync(branch);
        if (result == null) throw new NotFoundException("Sucursal no se ha creado.");

        return mapper.Map<ResponseBranchDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchDto> UpdateBranchAsync(byte id, RequestBranchDto branchDTO)
    {
        if (!await repository.ExistsBranchAsync(id)) throw new NotFoundException("Sucursal no encontrada.");

        var branch = await ValidateBranch(branchDTO);
        branch.Id = id;
        var result = await repository.UpdateBranchAsync(branch);

        return mapper.Map<ResponseBranchDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchDto> FindByIdAsync(byte id)
    {
        var branch = await repository.FindByIdAsync(id);
        if (branch == null) throw new NotFoundException("Sucursal no encontrada.");

        return mapper.Map<ResponseBranchDto>(branch);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseBranchDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    private async Task<Branch> ValidateBranch(RequestBranchDto branchDTO)
    {
        var branch = mapper.Map<Branch>(branchDTO);
        await branchValidator.ValidateAndThrowAsync(branch);
        return branch;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchDto>> ListAllByRoleAsync()
    {
        var user = await serviceUserAuthorization.GetLoggedUser();

        var list = await repository.ListAllByRoleAsync(user.Role.Description);
        var collection = mapper.Map<ICollection<ResponseBranchDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBranchAsync(byte id)
    {
        if (!await repository.ExistsBranchAsync(id)) throw new NotFoundException("Sucursal no encontrada.");
        return await repository.DeleteBranchAsync(id);
    }
}