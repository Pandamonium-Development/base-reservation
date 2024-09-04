using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursal(BaseReservationContext context) : IRepositorySucursal
{
    /// <summary>
    /// Create branch
    /// </summary>
    /// <param name="sucursal">Branch model to be added</param>
    /// <returns>Sucursal</returns>
    public async Task<Sucursal> CreateSucursalAsync(Sucursal sucursal)
    {
        var result = context.Sucursals.Add(sucursal);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Update branch
    /// </summary>
    /// <param name="sucursal">Branch model to be updated</param>
    /// <returns>Sucursal</returns>
    public async Task<Sucursal> UpdateSucursalAsync(Sucursal sucursal)
    {
        context.Sucursals.Update(sucursal);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(sucursal.Id);
        return response!;
    }

    /// <summary>
    /// Get branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Sucursal if founded, otherwise null</returns>
    public async Task<Sucursal?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Sucursal))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Sucursal>()
            .AsNoTracking()
            .Include(m => m.IdDistritoNavigation)
            .ThenInclude(m => m.IdCantonNavigation)
            .ThenInclude(m => m.IdProvinciaNavigation)
            .Include(m => m.UsuarioSucursals)
            .ThenInclude(m => m.IdUsuarioNavigation)
            .ThenInclude(m => m.IdRolNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Validate if exists branch with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsSucursalAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Sucursal))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Sucursal>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Get list of all branches
    /// </summary>
    /// <returns>ICollection of Sucursal</returns>
    public async Task<ICollection<Sucursal>> ListAllAsync()
    {
        var collection = await context.Set<Sucursal>()
            .AsNoTracking()
            .Include(a => a.IdDistritoNavigation)
            .ThenInclude(m => m.IdCantonNavigation)
            .ThenInclude(m => m.IdProvinciaNavigation)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all branches by role
    /// </summary>
    /// <param name="rol">Role to look for</param>
    /// <returns>ICollection of Sucursal</returns>
    public async Task<ICollection<Sucursal>> ListAllByRoleAsync(string rol)
    {
        var usuarioSucursales = await context.Set<UsuarioSucursal>().AsNoTracking()
               .Include(m => m.IdUsuarioNavigation)
               .ThenInclude(m => m.IdRolNavigation)
               .Where(m => m.IdUsuarioNavigation.IdRolNavigation.Descripcion == rol).ToListAsync();

        if (usuarioSucursales == null) usuarioSucursales = new List<UsuarioSucursal>();
        var listadoSucursales = usuarioSucursales.Select(m => m.IdSucursal).Distinct().ToList();

        var collection = await context.Set<Sucursal>()
            .AsNoTracking()
            .Include(a => a.IdDistritoNavigation)
            .ThenInclude(m => m.IdCantonNavigation)
            .ThenInclude(m => m.IdProvinciaNavigation)
            .Where(m => listadoSucursales.Any(x => x == m.Id))
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}