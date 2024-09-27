using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryInventory(BaseReservationContext context) : IRepositoryInventory
{
    /// <inheritdoc />
    public async Task<Inventory> CreateInventoryAsync(Inventory inventory)
    {
        var result = context.Inventories.Add(inventory);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteInventoryAsync(short id)
    {
        var inventory = await FindByIdAsync(id);
        inventory!.Active = false;

        context.Inventories.Update(inventory);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsInventoryAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventory))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Inventory>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Active) != null;
    }

    /// <inheritdoc />
    public async Task<Inventory?> FindByIdAsync(short id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Inventory))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Inventory>()
            .AsNoTracking()
            .Include(m => m.InventoryProducts)
            .FirstOrDefaultAsync(a => EF.Property<short>(a, keyProperty.Name) == id && a.Active);
    }

    /// <inheritdoc />
    public async Task<ICollection<Inventory>> ListAllAsync()
    {
        var collection = await context.Set<Inventory>()
            .Where(a => a.Active)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Inventory>> ListAllByBranchAsync(byte branchId)
    {
        var collection = await context.Set<Inventory>()
            .Where(m => m.BranchId == branchId && m.Active)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<Inventory> UpdateInventoryAsync(Inventory inventory)
    {
        context.Inventories.Update(inventory);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(inventory.Id);
        return response!;
    }
}