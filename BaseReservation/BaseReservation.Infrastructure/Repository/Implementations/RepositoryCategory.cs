using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCategory(BaseReservationContext context) : IRepositoryCategory
{
    /// <inheritdoc />
    public async Task<Category?> FindByIdAsync(byte id)
    {
        return await context.Set<Category>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Category>> ListAllAsync()
    {
        var collection = await context.Set<Category>().AsNoTracking().ToListAsync();
        return collection;
    }
}