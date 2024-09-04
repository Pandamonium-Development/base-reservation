using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryServicio
{
    Task<ICollection<Servicio>> ListAllAsync();

    Task<Servicio?> FindByIdAsync(byte id);

    Task<Servicio> CreateServicioAsync(Servicio servicio);

    Task<Servicio> UpdateServicioAsync(Servicio servicio);

    Task<bool> ExistsServicioAsync(byte id);
}