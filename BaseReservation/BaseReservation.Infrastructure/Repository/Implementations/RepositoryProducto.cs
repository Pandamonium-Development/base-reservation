using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProducto(BaseReservationContext context) : IRepositoryProducto
{
    /// <inheritdoc />
    public async Task<Producto> CreateProductoAsync(Producto producto)
    {
        var result = context.Productos.Add(producto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Producto> UpdateProductoAsync(Producto producto)
    {
        context.Productos.Update(producto);
        await context.SaveChangesAsync();
        var response = await FindByIdAsync(producto.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Producto?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Producto>()
            .Include(a => a.IdUnidadMedidaNavigation)
            .Include(a => a.IdCategoriaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Producto>> ListAllAsync(bool excludeProductosInventario = false, short idInventario = 0)
    {
        if (!excludeProductosInventario)
        {
            var collection = await context.Set<Producto>()
            .AsNoTracking()
            .ToListAsync();
            return collection;
        }

        var productosInventarioProducto = from a in context.Productos.AsQueryable()
                                          join b in context.InventarioProductos.AsQueryable() on a.Id equals b.IdProducto
                                          join c in context.Inventarios.AsQueryable() on b.IdInventario equals c.Id
                                          where c.Id == idInventario
                                          select a;

        return await context.Set<Producto>().Except(productosInventarioProducto.AsQueryable()).AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsProductoAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Producto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}