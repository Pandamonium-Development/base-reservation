using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUnidadMedida(BaseReservationContext context) : IRepositoryUnidadMedida
{
    /// <inheritdoc />
    public async Task<UnidadMedida?> FindByIdAsync(byte id)
    {
        return await context.Set<UnidadMedida>().FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ICollection<UnidadMedida>> ListAllAsync()
    {
        //AsQueryable hace el acceso más sencillo
        var collection = from unidades in context.Set<UnidadMedida>().AsQueryable()
                         select unidades;
        return await collection.AsNoTracking().ToListAsync();
    }
}