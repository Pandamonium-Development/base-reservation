using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceBranchScheduleBlock(IRepositoryBranchScheduleBlock repository,
                                                 IValidator<BranchScheduleBlock> blockValidator, IMapper mapper) : IServiceBranchScheduleBlock
{
    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> CreateBranchScheduleBlockAsync(RequestBranchScheduleBlockDto blockDto)
    {
        var block = await ValidateBranchScheduleBlock(blockDto);

        var result = await repository.CreateBranchScheduleBlockAsync(block);
        if (result == null) throw new NotFoundException("Horario bloqueo no se ha creado.");

        return mapper.Map<ResponseBranchScheduleBlockDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleBlockAsync(short idSucursalHorario, IEnumerable<RequestBranchScheduleBlockDto> blocks)
    {
        var blocksGuardar = await ValidateBranchScheduleBlock(idSucursalHorario, blocks);

        var result = await repository.CreateBranchScheduleBlockAsync(idSucursalHorario, blocksGuardar);
        if (!result) throw new ListNotAddedException("Error al guardar bloqueos");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> FindByIdAsync(long id)
    {
        var block = await repository.FindByIdAsync(id);
        if (block == null) throw new NotFoundException("Horario bloqueo no encontrado.");

        return mapper.Map<ResponseBranchScheduleBlockDto>(block);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseBranchScheduleBlockDto>> ListAllByBranchScheduleAsync(short idSucursalHorario)
    {
        var blocks = await repository.ListAllByBranchScheduleAsync(idSucursalHorario);

        return mapper.Map<ICollection<ResponseBranchScheduleBlockDto>>(blocks);
    }

    /// <inheritdoc />
    public async Task<ResponseBranchScheduleBlockDto> UpdateBranchScheduleBlockAsync(long id, RequestBranchScheduleBlockDto blockDto)
    {
        if (!await repository.ExistsBranchScheduleBlockAsync(id)) throw new NotFoundException("Horario bloqueo no encontrada.");

        var block = await ValidateBranchScheduleBlock(blockDto);
        block.Id = id;
        var result = await repository.UpdateBranchScheduleBlockAsync(block);

        return mapper.Map<ResponseBranchScheduleBlockDto>(result);
    }

    /// <inheritdoc />
    private async Task<BranchScheduleBlock> ValidateBranchScheduleBlock(RequestBranchScheduleBlockDto blockDTO)
    {
        var block = mapper.Map<BranchScheduleBlock>(blockDTO);
        await blockValidator.ValidateAndThrowAsync(block);
        return block;
    }

    /// <summary>
    /// Validate branch schedule's blocks
    /// </summary>
    /// <param name="idSucursalHorario">Branch schedule id that receive blocks</param>
    /// <param name="blocksDto">List of branch schedule's blocks request model will be validated</param>
    /// <returns>IEnumerable of BranchScheduleBlock</returns>
    private async Task<IEnumerable<BranchScheduleBlock>> ValidateBranchScheduleBlock(short idSucursalHorario, IEnumerable<RequestBranchScheduleBlockDto> blocksDto)
    {
        var blocks = mapper.Map<List<BranchScheduleBlock>>(blocksDto);
        foreach (var item in blocks)
        {
            item.Id = 0;
            item.BranchScheduleId = idSucursalHorario;
            await blockValidator.ValidateAndThrowAsync(item);
        }
        return blocks;
    }
}