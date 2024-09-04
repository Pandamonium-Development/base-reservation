using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryTokenMaster(BaseReservationContext context) : IRepositoryTokenMaster
{
    /// <summary>
    /// Create the token of an user to provide access
    /// </summary>
    /// <param name="tokenMaster">TokenMaster model to be added</param>
    /// <returns>TokenMaster</returns>
    public async Task<TokenMaster> CreateTokenMasterAsync(TokenMaster tokenMaster)
    {
        var result = context.TokenMasters.Add(tokenMaster);
        await context.SaveChangesAsync();

        return result.Entity;
    }

    /// <summary>
    /// Validate if the token already exists
    /// </summary>
    /// <param name="token">Token value</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsTokenMasterAsync(string token)
    {
        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Token == token) != null;
    }

    /// <summary>
    /// Get token with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>TokenMaster if founded, otherwise null</returns>
    public async Task<TokenMaster?> FindByIdAsync(long id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(TokenMaster))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<long>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Get token with specific token value
    /// </summary>
    /// <param name="token">Token to look for</param>
    /// <returns>TokenMaster if founded, otherwise null</returns>
    public async Task<TokenMaster?> FindByTokenAsync(string token)
    {
        return await context.Set<TokenMaster>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Token == token);
    }

    /// <summary>
    /// Update a token
    /// </summary>
    /// <param name="tokenMaster">TokenMaster model to be updated</param>
    /// <returns>TokenMaster</returns>
    public async Task<TokenMaster> UpdateTokenMasterAsync(TokenMaster tokenMaster)
    {
        context.TokenMasters.Update(tokenMaster);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(tokenMaster.Id);
        return response!;
    }
}