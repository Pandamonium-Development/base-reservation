
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUser(BaseReservationContext context) : IRepositoryUser
{
    /// <inheritdoc />
    public async Task<User?> FindByIdAsync(short id) => await context.Set<User>().FindAsync(id);

    /// <inheritdoc />
    public async Task<User?> FindByEmailAsync(string correoElectronico) => await context.Set<User>().Include(m => m.RoleIdNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.Email == correoElectronico);

    /// <inheritdoc />
    public async Task<ICollection<User>> ListAllAsync()
    {
        var collection = await context.Set<User>()
            .Include(a => a.RoleIdNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<User>> ListAllByRoleAsync(byte roleId)
    {
        var collection = await context.Set<User>()
            .Include(a => a.RoleIdNavigation)
            .AsNoTracking()
            .Where(m => m.RoleId == roleId)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<User?> LoginAsync(string email, string password) => await context.Set<User>().Include(m => m.RoleIdNavigation).AsNoTracking().FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

    /// <inheritdoc />
    public async Task<bool> IsAvailableAsync(short id, byte branchId) => !await context.Set<UserBranch>().AsNoTracking().AnyAsync(m => m.UserId == id && m.BranchId != branchId);

    /// <inheritdoc />
    public async Task<bool> ExistsUserAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(User))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}