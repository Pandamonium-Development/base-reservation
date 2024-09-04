using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventario(BaseReservationContext context) : IRepositoryInventario
{
    public async Task<Inventario> CreateInventarioAsync(Inventario inventario)
    {
        var result = context.Inventarios.Add(inventario);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> DeleteInventarioAsync(short id)
    {
        var inventario = await FindByIdAsync(id);
        inventario!.Activo = false;

        context.Inventarios.Update(inventario);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> ExisteInventario(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Inventario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    public async Task<Inventario?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Inventario>()
            .AsNoTracking()
            .Include(m => m.InventarioProductos)
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo);
    }

    public async Task<ICollection<Inventario>> ListAsync()
    {
        var collection = await context.Set<Inventario>()
            .Where(a => a.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    public async Task<ICollection<Inventario>> ListAsync(byte idSucursal)
    {
        var collection = await context.Set<Inventario>()
            .Where(m => m.IdSucursal == idSucursal && m.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    public async Task<Inventario> UpdateInventarioAsync(Inventario inventario)
    {
        context.Inventarios.Update(inventario);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(inventario.Id);
        return response!;
    }
}