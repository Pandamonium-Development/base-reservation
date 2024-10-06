using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProvince(BaseReservationContext context) : IRepositoryProvince
{
    /// <inheritdoc />
    public async Task<Province?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Province))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Province>().AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<ICollection<Province>> ListAllAsync()
    {
        var collection = await context.Set<Province>().AsNoTracking().ToListAsync();
        return collection;
    }
}