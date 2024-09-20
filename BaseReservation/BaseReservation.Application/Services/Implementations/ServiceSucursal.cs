using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Application.Services.Interfaces.Authorization;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSucursal(IRepositorySucursal repository, IMapper mapper,
                            IValidator<Sucursal> branchValidator, 
                            IServiceUserAuthorization serviceUserAuthorization) : IServiceSucursal
{
    /// <inheritdoc />
    public async Task<ResponseSucursalDto> CreateBranchAsync(RequestSucursalDto branchDTO)
    {
        var branch = await ValidateBranch(branchDTO);

        var result = await repository.CreateBranchAsync(branch);
        if (result == null) throw new NotFoundException("Sucursal no se ha creado.");

        return mapper.Map<ResponseSucursalDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseSucursalDto> UpdateBranchAsync(byte id, RequestSucursalDto branchDTO)
    {
        if (!await repository.ExistsBranchAsync(id)) throw new NotFoundException("Sucursal no encontrada.");

        var branch = await ValidateBranch(branchDTO);
        branch.Id = id;
        var result = await repository.UpdateBranchAsync(branch);

        return mapper.Map<ResponseSucursalDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseSucursalDto> FindByIdAsync(byte id)
    {
        var branch = await repository.FindByIdAsync(id);
        if (branch == null) throw new NotFoundException("Sucursal no encontrada.");

        return mapper.Map<ResponseSucursalDto>(branch);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseSucursalDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseSucursalDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    private async Task<Sucursal> ValidateBranch(RequestSucursalDto branchDTO)
    {
        var branch = mapper.Map<Sucursal>(branchDTO);
        await branchValidator.ValidateAndThrowAsync(branch);
        return branch;
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseSucursalDto>> ListAllByRolAsync()
    {
        var user = await serviceUserAuthorization.GetLoggedUser();

        var list = await repository.ListAllByRoleAsync(user.Rol.Descripcion);
        var collection = mapper.Map<ICollection<ResponseSucursalDto>>(list);

        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBranchAsync(byte id)
    {
        if (!await repository.ExistsBranchAsync(id)) throw new NotFoundException("Sucursal no encontrada.");
        return await repository.DeleteBranchAsync(id);
    }
}