using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTypeService(BaseReservationContext context) : IRepositoryTypeService
{
    /// <inheritdoc />
    public async Task<TypeService?> FindByIdAsync(byte id) => await context.Set<TypeService>().FindAsync(id);

    /// <inheritdoc />
    public async Task<ICollection<TypeService>> ListAllAsync()
    {
        var collection = await context.Set<TypeService>().AsNoTracking().ToListAsync();
        return collection;
    }
}