using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Azure;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUserBranch(BaseReservationContext context) : IRepositoryUserBranch
{
    /// <inheritdoc />
    public async Task<bool> AssignUsersAsync(byte branchId, IEnumerable<UserBranch> usersBranch)
    {
        var result = true;
        var existingUsersBranch = await ListAllByBranchAsync(branchId);

        var executionStrategy = context.Database.CreateExecutionStrategy();

        await executionStrategy.Execute(async () =>
        {
            using var transaccion = await context.Database.BeginTransactionAsync();
            try
            {
                context.UserBranches.RemoveRange(existingUsersBranch);
                var rowsAffected = await context.SaveChangesAsync();

                if (rowsAffected == 0 && existingUsersBranch.Any())
                {
                    await transaccion.RollbackAsync();
                    result = false;
                }
                else
                {
                    context.UserBranches.AddRange(usersBranch);
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
                throw new RequestFailedException("Error al guardar usuarios", exc);
            }
        });

        return result;
    }

    /// <inheritdoc />    
    public async Task<ICollection<UserBranch>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<UserBranch>()
           .AsNoTracking()
           .Where(m => m.BranchId == branchId)
           .ToListAsync();
        return collection;
    }
}