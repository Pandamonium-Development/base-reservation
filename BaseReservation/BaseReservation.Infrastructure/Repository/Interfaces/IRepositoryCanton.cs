using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryCanton
{
    Task<ICollection<Canton>> ListAllByProvinciaAsync(byte idProvincia);

    Task<Canton?> FindByIdAsync(byte id);
}