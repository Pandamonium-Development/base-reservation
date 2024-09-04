using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProveedor(BaseReservationContext context) : IRepositoryProveedor
{
    /// <summary>
    /// Creates a new Proveedor entity in the database.
    /// </summary>
    /// <param name="proveedor">The Proveedor entity to be created.</param>
    /// <returns>Proveedor</returns>
    public async Task<Proveedor> CreateProveedorAsync(Proveedor proveedor)
    {
        var result = context.Proveedors.Add(proveedor);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Marks a Proveedor entity as inactive instead of deleting it from the database.
    /// </summary>
    /// <param name="id">The unique identifier of the Proveedor to be marked as inactive.</param>
    /// <returns>The unique identifier of the Inventory to delete.</</returns>
    public async Task<bool> DeleteProveedorAsync(byte id)
    {
        var proveedor = await FindByIdAsync(id);
        proveedor!.Activo = false;

        context.Proveedors.Update(proveedor);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <summary>
    /// Checks if a Proveedor with the specified ID exists and is active.
    /// </summary>
    /// <param name="id">The unique identifier of the Proveedor.</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsProveedorAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Proveedor))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Proveedor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <summary>
    ///  Retrieves a Proveedor entity by its ID, including related Contactos and location entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Proveedor if founded, otherwise null</returns>
    public async Task<Proveedor?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Proveedor))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Proveedor>()
            .Include(m => m.Contactos)
            .Include(m => m.IdDistritoNavigation)
            .ThenInclude(m => m.IdCantonNavigation)
            .ThenInclude(m => m.IdProvinciaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo);
    }

    /// <summary>
    /// Retrieves all active Proveedor entities, including their related location entities.
    /// </summary>
    /// <returns>ICollection of Proveedor</returns>
    public async Task<ICollection<Proveedor>> ListAllAsync()
    {
        var collection = await context.Set<Proveedor>()
            .Include(m => m.IdDistritoNavigation)
            .ThenInclude(m => m.IdCantonNavigation)
            .ThenInclude(m => m.IdProvinciaNavigation)
            .AsNoTracking()
            .Where(m => m.Activo)
            .ToListAsync();

        return collection;
    }
    /// <summary>
    /// Retrieves all active Proveedor entities as an IQueryable for deferred execution.
    /// </summary>
    /// <returns>An IQueryable of active Proveedor entities.</returns>
    public IQueryable<Proveedor> ListAllQueryable() => context.Set<Proveedor>().Where(m => m.Activo).AsNoTracking();

    /// <summary>
    /// Updates an existing Proveedor entity in the database.
    /// </summary>
    /// <param name="proveedor">The Proveedor entity with updated information.</param>
    /// <returns>Proveedor</returns>
    public async Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor)
    {
        context.Proveedors.Update(proveedor);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(proveedor.Id);
        return response!;
    }
}