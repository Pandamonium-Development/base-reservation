using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryImpuesto(BaseReservationContext context) : IRepositoryImpuesto
{
    public async Task<ICollection<Impuesto>> ListAllAsync() => await context.Set<Impuesto>().AsNoTracking().ToListAsync();
}