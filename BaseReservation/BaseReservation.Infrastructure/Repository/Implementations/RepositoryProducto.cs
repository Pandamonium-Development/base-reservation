using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProducto(BaseReservationContext context) : IRepositoryProducto
{
    /// <summary>
    /// Creates a new Producto entity in the database.
    /// </summary>
    /// <param name="producto">The Producto entity to be created.</param>
    /// <returns></returns>
    public async Task<Producto> CreateProductoAsync(Producto producto)
    {
        var result = context.Productos.Add(producto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Updates an existing Producto entity in the database.
    /// </summary>
    /// <param name="producto">The Producto entity with update information.</param>
    /// <returns></returns>
    public async Task<Producto> UpdateProductoAsync(Producto producto)
    {
        context.Productos.Update(producto);
        await context.SaveChangesAsync();
        var response = await FindByIdAsync(producto.Id);
        return response!;
    }

    /// <summary>
    /// Retrieves a Producto entity by its ID, including related UnidadMedida and Categoria entities.
    /// </summary>
    /// <param name="id">The unique identifier of the Producto.</param>
    /// <returns></returns>
    public async Task<Producto?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Producto>()
            .Include(a => a.IdUnidadMedidaNavigation)
            .Include(a => a.IdCategoriaNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Retrieves all Producto entities, optionally excluding those present in a specific Inventario. }
    /// Default is false.
    /// </summary>
    /// <param name="excludeProductosInventario">Whether to exclude products that are in a specific Inventario.  </param>
    /// <param name="idInventario"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Checks if a Producto with the specified ID exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the Producto.</param>
    /// <returns></returns>
    public async Task<bool> ExistsProductoAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Producto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Producto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}