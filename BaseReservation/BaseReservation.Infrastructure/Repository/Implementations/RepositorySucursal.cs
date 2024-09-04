using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositorySucursal(BaseReservationContext context) : IRepositorySucursal
{
    public async Task<Sucursal> CreateSucursalAsync(Sucursal sucursal)
    {
        var result = context.Sucursals.Add(sucursal);
        //aplica en la BD
        await context.SaveChangesAsync();
        //Refleja la entidad
        return result.Entity;
    }

    public async Task<Sucursal> UpdateSucursalAsync(Sucursal sucursal)
    {
        context.Sucursals.Update(sucursal);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(sucursal.Id);
        return response!;
    }

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

    public async Task<bool> ExisteSucursal(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Sucursal))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Sucursal>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id) != null;
    }

    public async Task<ICollection<Sucursal>> ListAsync()
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

    public async Task<ICollection<Sucursal>> ListAsync(string rol)
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