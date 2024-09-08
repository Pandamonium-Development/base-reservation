using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProveedor(BaseReservationContext context) : IRepositoryProveedor
{
    /// <inheritdoc />
    public async Task<Proveedor> CreateProveedorAsync(Proveedor proveedor)
    {
        var result = context.Proveedors.Add(proveedor);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteProveedorAsync(byte id)
    {
        var proveedor = await FindByIdAsync(id);
        proveedor!.Activo = false;

        context.Proveedors.Update(proveedor);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsProveedorAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Proveedor))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Proveedor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Activo) != null;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    public IQueryable<Proveedor> ListAllQueryable() => context.Set<Proveedor>().Where(m => m.Activo).AsNoTracking();

    /// <inheritdoc />
    public async Task<Proveedor> UpdateProveedorAsync(Proveedor proveedor)
    {
        context.Proveedors.Update(proveedor);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(proveedor.Id);
        return response!;
    }
}