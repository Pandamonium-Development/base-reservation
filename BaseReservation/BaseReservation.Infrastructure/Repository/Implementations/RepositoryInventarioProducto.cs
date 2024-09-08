using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventarioProducto(BaseReservationContext context) : IRepositoryInventarioProducto
{
    /// <inheritdoc />
    public async Task<InventarioProducto> CreateProductoInventarioAsync(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProductos.Add(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> CreateProductoInventarioAsync(IEnumerable<InventarioProducto> inventarioProducto)
    {
        context.InventarioProductos.AddRange(inventarioProducto);
        var filasAfectadas = await context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsInventarioProductoAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<InventarioProducto?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<InventarioProducto>> ListAllByInventarioAsync(short idInventario) =>
        await context.InventarioProductos.Include(m => m.IdProductoNavigation)
        .Where(m => m.IdInventario == idInventario).AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<ICollection<InventarioProducto>> ListAllByProductoAsync(short idProducto) =>
        await context.InventarioProductos.Where(m => m.IdProducto == idProducto).ToListAsync();

    /// <inheritdoc />
    public async Task<InventarioProducto> UpdateProductoInventarioAsync(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProductos.Update(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }
}