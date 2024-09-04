using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventarioProducto(BaseReservationContext context) : IRepositoryInventarioProducto
{
    /// <summary>
    /// Creates a new InventarioProducto
    /// </summary>
    /// <param name="inventarioProducto">The InventarioProdcuto entity to be added.</param>
    /// <returns>InventarioProducto</returns>
    public async Task<InventarioProducto> CreateProductoInventarioAsync(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProductos.Add(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Creates multiple InventarioProducto entities.
    /// </summary>
    /// <param name="inventarioProducto">A collection of InventarioProducto entities to be added.</param>
    /// <returns>True if were added, if not, false</returns>
    public async Task<bool> CreateProductoInventarioAsync(IEnumerable<InventarioProducto> inventarioProducto)
    {
        context.InventarioProductos.AddRange(inventarioProducto);
        var filasAfectadas = await context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    /// <summary>
    /// Checks if an InventarioProducto with the specified identifier exists.
    /// </summary>
    /// <param name="id"> The unique identifier of the InventarioProducto.</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsInventarioProductoAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Finds an InventarioProdcuto by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the InventarioProducto</param>
    /// <returns>InventarioProducto if founded, otherwise null</returns>
    public async Task<InventarioProducto?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventarioProducto))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventarioProducto>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Lists all InventarioProducto entities associated with a specific Inventario.
    /// </summary>
    /// <param name="idInventario">tThe unique identifier of the Inventario.</param>
    /// <returns>ICollection of InventarioProducto</returns>
    public async Task<ICollection<InventarioProducto>> ListAllByInventarioAsync(short idInventario) =>
        await context.InventarioProductos.Include(m => m.IdProductoNavigation)
        .Where(m => m.IdInventario == idInventario).AsNoTracking().ToListAsync();

    /// <summary>
    /// Lists all InventarioProducto entities associated with a specific Producto.
    /// </summary>
    /// <param name="idProducto">The unique identifier of the Producto.</param>
    /// <returns>ICollection of InventarioProducto </returns>
    public async Task<ICollection<InventarioProducto>> ListAllByProductoAsync(short idProducto) =>
        await context.InventarioProductos.Where(m => m.IdProducto == idProducto).ToListAsync();

    /// <summary>
    /// Updates an existing InventarioProducto entity.
    /// </summary>
    /// <param name="inventarioProducto">The InventarioProducto entity to update.</param>
    /// <returns>InventarioProducto</returns>
    public async Task<InventarioProducto> UpdateProductoInventarioAsync(InventarioProducto inventarioProducto)
    {
        var result = context.InventarioProductos.Update(inventarioProducto);
        await context.SaveChangesAsync();
        return result.Entity;
    }
}