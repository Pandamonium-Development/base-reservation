using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursal(BaseReservationContext context) : IRepositorySucursal
{
    /// <inheritdoc />
    public async Task<Sucursal> CreateBranchAsync(Sucursal branch)
    {
        var result = context.Sucursals.Add(branch);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Sucursal> UpdateBranchAsync(Sucursal branch)
    {
        context.Sucursals.Update(branch);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(branch.Id);
        return response!;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<bool> ExistsBranchAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Sucursal))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Sucursal>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task<bool> DeleteBranchAsync(byte id)
    {
        var branch = await FindByIdAsync(id);
        branch!.Activo = false;

        context.Sucursals.Update(branch);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }
}