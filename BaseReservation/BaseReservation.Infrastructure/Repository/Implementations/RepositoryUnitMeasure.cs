using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUnitMeasure(BaseReservationContext context) : IRepositoryUnitMeasure
{
    /// <inheritdoc />
    public async Task<UnitMeasure?> FindByIdAsync(byte id) => await context.Set<UnitMeasure>().FindAsync(id);

    /// <inheritdoc />
    public async Task<ICollection<UnitMeasure>> ListAllAsync()
    {
        var collection = from unidades in context.Set<UnitMeasure>().AsQueryable()
                         select unidades;
        return await collection.AsNoTracking().ToListAsync();
    }
}