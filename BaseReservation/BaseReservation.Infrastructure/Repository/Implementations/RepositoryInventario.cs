using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventario(BaseReservationContext context) : IRepositoryInventario
{
    /// <summary>
    /// Create a new Inventory.
    /// </summary>
    /// <param name="inventario">The Inventory entity to be added.</param>
    /// <returns>Inventario</returns>
    public async Task<Inventario> CreateInventarioAsync(Inventario inventario)
    {
        var result = context.Inventarios.Add(inventario);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Delete an Inventory by setting its "Activo" property to false
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory to delete.</param>
    /// <returns>A boolean indicating whether the operation was successful.</returns>
    public async Task<bool> DeleteInventarioAsync(short id)
    {
        var inventario = await FindByIdAsync(id);
        inventario!.Activo = false;

        context.Inventarios.Update(inventario);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <summary>
    /// Checks if an active Inventory with the specified identifier exists.
    /// </summary>
    /// <param name="id">The unique identifier of the Inventory</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsInventarioAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Inventario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <summary>
    /// Finds an active Inventory by its unique identifier, including related Inventory Products.
    /// </summary>
    /// <param name="id">The Inventory entity if found. </param>
    /// <returns>Inventario if founded, otherwise null</returns>
    public async Task<Inventario?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventario))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Inventario>()
            .AsNoTracking()
            .Include(m => m.InventarioProductos)
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <summary>
    /// Lists all active Inventory entities.
    /// </summary>
    /// <returns>ICollection of Iventario</returns>
    public async Task<ICollection<Inventario>> ListAllAsync()
    {
        var collection = await context.Set<Inventario>()
            .Where(a => a.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Lists all active Inventory entities for a specific branch.
    /// </summary>
    /// <param name="idSucursal"></param>
    /// <returns>ICollection of Iventario</returns>
    public async Task<ICollection<Inventario>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<Inventario>()
            .Where(m => m.IdSucursal == idSucursal && m.Activo)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Updates an existing Inventory
    /// </summary>
    /// <param name="inventario"> The Inventory entity to update.</param>
    /// <returns>Inventario</returns>
    public async Task<Inventario> UpdateInventarioAsync(Inventario inventario)
    {
        context.Inventarios.Update(inventario);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(inventario.Id);
        return response!;
    }
}