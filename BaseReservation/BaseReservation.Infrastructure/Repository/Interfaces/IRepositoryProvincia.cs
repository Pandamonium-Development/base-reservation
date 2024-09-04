using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryProvincia
{
    Task<ICollection<Provincia>> ListAllAsync();

    Task<Provincia?> FindByIdAsync(byte id);
}
