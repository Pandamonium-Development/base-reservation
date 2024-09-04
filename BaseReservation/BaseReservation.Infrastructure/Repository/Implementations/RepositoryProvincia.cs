using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryProvincia(BaseReservationContext context) : IRepositoryProvincia
{
    public async Task<Provincia?> FindByIdAsync(byte id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Provincia))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Provincia>().AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<byte>(a, keyProperty.Name) == id);
    }

    public async Task<ICollection<Provincia>> ListAsync()
    {
        var collection = await context.Set<Provincia>().AsNoTracking().ToListAsync();
        return collection;
    }
}