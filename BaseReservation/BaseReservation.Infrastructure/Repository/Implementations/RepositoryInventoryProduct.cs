using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventoryProduct(BaseReservationContext context) : IRepositoryInventoryProduct
{
    /// <inheritdoc />
    public async Task<InventoryProduct> CreateInventoryProductAsync(InventoryProduct inventoryProduct)
    {
        var result = context.InventoryProducts.Add(inventoryProduct);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> CreateInventoryProductAsync(IEnumerable<InventoryProduct> inventoryProduct)
    {
        context.InventoryProducts.AddRange(inventoryProduct);
        var filasAfectadas = await context.SaveChangesAsync();
        return filasAfectadas > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsInventoryProductAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventoryProduct))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventoryProduct>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<InventoryProduct?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(InventoryProduct))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<InventoryProduct>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<InventoryProduct>> ListAllByInventoryAsync(short inventoryId) =>
        await context.InventoryProducts.Include(m => m.ProductIdNavigation)
        .Where(m => m.InventoryId == inventoryId).AsNoTracking().ToListAsync();

    /// <inheritdoc />
    public async Task<ICollection<InventoryProduct>> ListAllByProductAsync(short productId) =>
        await context.InventoryProducts.Where(m => m.ProductId == productId).ToListAsync();

    /// <inheritdoc />
    public async Task<InventoryProduct> UpdateInventoryProductAsync(InventoryProduct inventoryProduct)
    {
        var result = context.InventoryProducts.Update(inventoryProduct);
        await context.SaveChangesAsync();
        return result.Entity;
    }
}