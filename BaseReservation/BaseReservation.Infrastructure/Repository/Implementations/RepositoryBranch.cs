using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryBranch(BaseReservationContext context) : IRepositoryBranch
{
    /// <inheritdoc />
    public async Task<Branch> CreateBranchAsync(Branch branch)
    {
        var result = context.Branches.Add(branch);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Branch> UpdateBranchAsync(Branch branch)
    {
        context.Branches.Update(branch);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(branch.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Branch?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Branch))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Branch>()
            .AsNoTracking()
            .Include(m => m.DistrictIdNavigation)
            .ThenInclude(m => m.CantonIdNavigation)
            .ThenInclude(m => m.ProvinceIdNavigation)
            .Include(m => m.UserBranches)
            .ThenInclude(m => m.UserIdNavigation)
            .ThenInclude(m => m.RoleIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsBranchAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Branch))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Branch>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<ICollection<Branch>> ListAllAsync()
    {
        var collection = await context.Set<Branch>()
            .AsNoTracking()
            .Include(m => m.DistrictIdNavigation)
            .ThenInclude(m => m.CantonIdNavigation)
            .ThenInclude(m => m.ProvinceIdNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Branch>> ListAllByRoleAsync(string rol)
    {
        var userBranches = await context.Set<UserBranch>().AsNoTracking()
               .Include(m => m.UserIdNavigation)
               .ThenInclude(m => m.RoleIdNavigation)
               .Where(m => m.UserIdNavigation.RoleIdNavigation.Description == rol).ToListAsync();

        if (userBranches == null) userBranches = new List<UserBranch>();
        var branchList = userBranches.Select(m => m.BranchId).Distinct().ToList();

        var collection = await context.Set<Branch>()
            .AsNoTracking()
            .Include(m => m.DistrictIdNavigation)
            .ThenInclude(m => m.CantonIdNavigation)
            .ThenInclude(m => m.ProvinceIdNavigation)
            .Where(m => branchList.Any(x => x == m.Id))
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteBranchAsync(byte id)
    {
        var branch = await FindByIdAsync(id);
        branch!.Active = false;

        context.Branches.Update(branch);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }
}