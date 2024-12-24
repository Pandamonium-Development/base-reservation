using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryRole(BaseReservationContext context) : IRepositoryRole
{
    /// <inheritdoc />
    public async Task<Role?> FindByIdAsync(byte id) => await context.Set<Role>().FindAsync(id);

    /// <inheritdoc />
    public async Task<ICollection<Role>> ListAllAsync() => await context.Set<Role>().AsNoTracking().ToListAsync();
}