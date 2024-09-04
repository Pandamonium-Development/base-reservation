using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryImpuesto(BaseReservationContext context) : IRepositoryImpuesto
{
    /// <summary>
    /// Lists all Tax entities
    /// </summary>
    /// <returns>ICollection of Impuesto</returns>
    public async Task<ICollection<Impuesto>> ListAllAsync() => await context.Set<Impuesto>().AsNoTracking().ToListAsync();
}