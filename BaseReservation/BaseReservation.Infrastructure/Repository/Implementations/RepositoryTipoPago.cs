using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTipoPago(BaseReservationContext context) : IRepositoryTipoPago
{
    public async Task<ICollection<TipoPago>> ListAllAsync() => await context.Set<TipoPago>().AsNoTracking().ToListAsync();
}