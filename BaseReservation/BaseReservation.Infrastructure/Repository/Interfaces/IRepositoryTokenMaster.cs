using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTokenMaster
{
    /// <summary>
    /// Create the token of an user to provide access
    /// </summary>
    /// <param name="tokenMaster">TokenMaster model to be added</param>
    /// <returns>TokenMaster</returns>
    Task<TokenMaster> CreateTokenMasterAsync(TokenMaster tokenMaster);

    /// <summary>
    /// Update a token
    /// </summary>
    /// <param name="tokenMaster">TokenMaster model to be updated</param>
    /// <returns>TokenMaster</returns>
    Task<TokenMaster> UpdateTokenMasterAsync(TokenMaster tokenMaster);

    /// <summary>
    /// Get token with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>TokenMaster if founded, otherwise null</returns>
    Task<TokenMaster?> FindByIdAsync(long id);

    /// <summary>
    /// Get token with specific token value
    /// </summary>
    /// <param name="token">Token to look for</param>
    /// <returns>TokenMaster if founded, otherwise null</returns>
    Task<TokenMaster?> FindByTokenAsync(string token);

    /// <summary>
    /// Validate if the token already exists
    /// </summary>
    /// <param name="token">Token value</param>
    /// <returns>True if exists, if not, false</returns>
    Task<bool> ExistsTokenMasterAsync(string token);
}