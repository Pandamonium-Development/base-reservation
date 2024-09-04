using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventarioProducto(BaseReservationContext context) : IRepositoryInventarioProducto
{
    public async Task<InventarioProducto> AgregarProductoInventario(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProducto.Add(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<bool> AgregarProductoInventario(IEnumerable<InventarioProducto> inventarioProducto)
    {
        context.InventarioProducto.AddRange(inventarioProducto);
        var filasAfectadas = await context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    public async Task<bool> ExisteInventarioProducto(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    public async Task<InventarioProducto?> GetInventarioProductoById(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    public async Task<ICollection<InventarioProducto>> ListByInventarioAsync(short idInventario) =>
        await context.InventarioProducto.Include(m => m.IdProductoNavigation).Where(m => m.IdInventario == idInventario).AsNoTracking().ToListAsync();

    public async Task<ICollection<InventarioProducto>> ListByProductoAsync(short idProducto) =>
        await context.InventarioProducto.Where(m => m.IdProducto == idProducto).ToListAsync();
    public async Task<InventarioProducto> UpdateProductoInventario(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProducto.Update(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }
}