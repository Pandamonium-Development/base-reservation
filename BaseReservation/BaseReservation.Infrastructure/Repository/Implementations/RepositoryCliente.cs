
using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryCliente(BaseReservationContext context) : IRepositoryCliente
{
    /// <inheritdoc />
    public async Task<ICollection<Cliente>> ListAllAsync() => await context.Set<Cliente>().AsNoTracking().ToListAsync();
}