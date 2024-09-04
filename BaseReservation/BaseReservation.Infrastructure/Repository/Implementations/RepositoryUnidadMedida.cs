using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryUnidadMedida(BaseReservationContext context) : IRepositoryUnidadMedida
{
    /// <summary>
    /// Get unit of measure with specific id
    /// </summary>
    /// <param name="id">Unit of measure id</param>
    /// <returns>Unit of measure if founded, otherwise null</returns>
    public async Task<UnidadMedida?> FindByIdAsync(byte id)
    {
        return await context.Set<UnidadMedida>().FindAsync(id);
    }

    /// <summary>
    /// Get list of all of units of measure 
    /// </summary>
    /// <returns>ICollection of UnidadMedida</returns>
    public async Task<ICollection<UnidadMedida>> ListAllAsync()
    {
        //AsQueryable hace el acceso más sencillo
        var collection = from unidades in context.Set<UnidadMedida>().AsQueryable()
                         select unidades;
        return await collection.AsNoTracking().ToListAsync();
    }
}