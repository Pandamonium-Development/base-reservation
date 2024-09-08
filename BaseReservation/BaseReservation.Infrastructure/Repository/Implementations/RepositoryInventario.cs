using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventario(BaseReservationContext context) : IRepositoryInventario
{
    /// <inheritdoc />
    public async Task<Inventario> CreateInventarioAsync(Inventario inventario)
    {
        var result = context.Inventarios.Add(inventario);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteInventarioAsync(short id)
    {
        var inventario = await FindByIdAsync(id);
        inventario!.Activo = false;

        context.Inventarios.Update(inventario);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsInventarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Inventario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <inheritdoc />
    public async Task<Inventario?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Inventario>()
            .AsNoTracking()
            .Include(m => m.InventarioProductos)
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <inheritdoc />
    public async Task<ICollection<Inventario>> ListAllAsync()
    {
        var collection = await context.Set<Inventario>()
            .Where(a => a.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Inventario>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<Inventario>()
            .Where(m => m.IdSucursal == idSucursal && m.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Inventario> UpdateInventarioAsync(Inventario inventario)
    {
        context.Inventarios.Update(inventario);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(inventario.Id);
        return response!;
    }
}