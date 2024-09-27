using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProduct(BaseReservationContext context) : IRepositoryProduct
{
    /// <inheritdoc />
    public async Task<Product> CreateProductAsync(Product producto)
    {
        var result = context.Products.Add(producto);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Product> UpdateProductAsync(Product producto)
    {
        context.Products.Update(producto);
        await context.SaveChangesAsync();
        var response = await FindByIdAsync(producto.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Product?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Product))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Product>()
            .Include(a => a.UnitMeasureIdNavigation)
            .Include(a => a.CategoryIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Product>> ListAllAsync(bool excludeProductsInventory = false, short inventoryId = 0)
    {
        if (!excludeProductsInventory)
        {
            var collection = await context.Set<Product>()
            .AsNoTracking()
            .ToListAsync();
            return collection;
        }

        var products = from a in context.Products.AsQueryable()
                       join b in context.InventoryProducts.AsQueryable() on a.Id equals b.ProductId
                       join c in context.Inventories.AsQueryable() on b.InventoryId equals c.Id
                       where c.Id == inventoryId
                       select a;

        return await context.Set<Product>().Except(products.AsQueryable()).AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsProductAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Product))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Product>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id) != null;
    }
}