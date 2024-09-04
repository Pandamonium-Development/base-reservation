using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTokenMaster(BaseReservationContext context) : IRepositoryTokenMaster
{
    public async Task<TokenMaster> CreateTokenMasterAsync(TokenMaster tokenMaster)
    {
        var result = context.TokenMasters.Add(tokenMaster);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<bool> ExisteTokenMaster(string token)
    {
        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Token == token) != null;
    }

    public async Task<TokenMaster?> GetTokenMasterAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(TokenMaster))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    public async Task<TokenMaster?> GetTokenMasterAsync(string token)
    {
        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Token == token);
    }

    public async Task<TokenMaster> UpdateTokenMasterAsync(TokenMaster tokenMaster)
    {
        context.TokenMasters.Update(tokenMaster);

        await context.SaveChangesAsync();

        var response = await GetTokenMasterAsync(tokenMaster.Id);
        return response!;
    }
}