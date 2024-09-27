using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryBranchScheduleBlock(BaseReservationContext context) : IRepositoryBranchScheduleBlock
{
    /// <inheritdoc />
    public async Task<BranchScheduleBlock> CreateBranchScheduleBlockAsync(BranchScheduleBlock branchScheduleBlock)
    {
        var result = context.BranchScheduleBlocks.Add(branchScheduleBlock);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> CreateBranchScheduleBlockAsync(short branchScheduleId, IEnumerable<BranchScheduleBlock> branchScheduleBlocks)
    {
        var result = true;
        var existingBlocks = await ListAllByBranchScheduleAsync(branchScheduleId);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.BranchScheduleBlocks.RemoveRange(existingBlocks);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && existingBlocks.Count != 0)
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.BranchScheduleBlocks.AddRange(branchScheduleBlocks);
                    rowsAffected = await context.SaveChangesAsync();

                    if (rowsAffected == 0)
                    {
                        await transaccion.RollbackAsync();
                        result = false;
                    }
                    else
                    {
                        await transaccion.CommitAsync();
                    }
                }

            }
            catch (Exception exc)
            {
                await transaccion.RollbackAsync();
                throw new RequestFailedException("Error al guardar feriados", exc);
            }
        });

        return result;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsBranchScheduleBlockAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(BranchScheduleBlock))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<BranchScheduleBlock>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<BranchScheduleBlock?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(BranchScheduleBlock))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<BranchScheduleBlock>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<BranchScheduleBlock>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<BranchScheduleBlock>()
         .Include(m => m.BranchScheduleIdNavigation)
         .ThenInclude(m => m.ScheduleIdNavigation)
         .Where(a => a.BranchScheduleIdNavigation.BranchId == branchId)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<BranchScheduleBlock>> ListAllByBranchScheduleAsync(short branchScheduleId)
    {
        var collection = await context.Set<BranchScheduleBlock>()
         .Where(a => a.BranchScheduleId == branchScheduleId)
         .AsNoTracking()
         .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<BranchScheduleBlock> UpdateBranchScheduleBlockAsync(BranchScheduleBlock branchScheduleBlock)
    {
        context.BranchScheduleBlocks.Update(branchScheduleBlock);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(branchScheduleBlock.Id);
        return response!;
    }
}