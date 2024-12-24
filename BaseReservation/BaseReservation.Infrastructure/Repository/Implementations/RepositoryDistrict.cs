using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryDistrict(BaseReservationContext context) : IRepositoryDistrict
{
    /// <inheritdoc />
    public async Task<District?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(District))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<District>()
                    .Include(m => m.CantonIdNavigation)
                    .ThenInclude(m => m.ProvinceIdNavigation)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<District>> ListAllByCantonAsync(byte cantonId)
    {
        var collection = await context.Set<District>()
            .Where(m => m.CantonId == cantonId)
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }
}