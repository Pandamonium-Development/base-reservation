using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryVendor(BaseReservationContext context) : IRepositoryVendor
{
    /// <inheritdoc />
    public async Task<Vendor> CreateVendorAsync(Vendor vendor)
    {
        var result = context.Vendors.Add(vendor);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteVendorAsync(byte id)
    {
        var vendor = await FindByIdAsync(id);
        vendor!.Active = false;

        context.Vendors.Update(vendor);

        var rowsAffected = await context.SaveChangesAsync();
        return rowsAffected > 0;
    }

    /// <inheritdoc />
    public async Task<bool> ExistsVendorAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Vendor))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Vendor>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active) != null;
    }

    /// <inheritdoc />
    public async Task<Vendor?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Vendor))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Vendor>()
            .Include(m => m.Contacts)
            .Include(m => m.DistrictIdNavigation)
            .ThenInclude(m => m.CantonIdNavigation)
            .ThenInclude(m => m.ProvinceIdNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id && a.Active);
    }

    /// <inheritdoc />
    public async Task<ICollection<Vendor>> ListAllAsync()
    {
        var collection = await context.Set<Vendor>()
            .Include(m => m.DistrictIdNavigation)
            .ThenInclude(m => m.CantonIdNavigation)
            .ThenInclude(m => m.ProvinceIdNavigation)
            .AsNoTracking()
            .Where(m => m.Active)
            .ToListAsync();

        return collection;
    }

    /// <inheritdoc />
    public IQueryable<Vendor> ListAllQueryable() => context.Set<Vendor>().Where(m => m.Active).AsNoTracking();

    /// <inheritdoc />
    public async Task<Vendor> UpdateVendorAsync(Vendor vendor)
    {
        context.Vendors.Update(vendor);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(vendor.Id);
        return response!;
    }
}