using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryTokenMaster
{
    Task<TokenMaster> CreateTokenMasterAsync(TokenMaster tokenMaster);

    Task<TokenMaster> UpdateTokenMasterAsync(TokenMaster tokenMaster);

    Task<TokenMaster?> FindByIdAsync(long id);

    Task<TokenMaster?> FindByTokenAsync(string token);

    Task<bool> ExistsTokenMaster(string token);
}