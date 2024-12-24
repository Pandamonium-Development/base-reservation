using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTax(BaseReservationContext context) : IRepositoryTax
{
    /// <inheritdoc />
    public async Task<ICollection<Tax>> ListAllAsync() => await context.Set<Tax>().AsNoTracking().ToListAsync();
}