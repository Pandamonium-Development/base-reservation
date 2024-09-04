using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTipoPago(BaseReservationContext context) : IRepositoryTipoPago
{
    /// <summary>
    /// Get list of all types of payment
    /// </summary>
    /// <returns>ICollection of TipoPago</returns>
    public async Task<ICollection<TipoPago>> ListAllAsync() => await context.Set<TipoPago>().AsNoTracking().ToListAsync();
}