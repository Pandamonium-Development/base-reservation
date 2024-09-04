using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalHorarioBloqueo
{
    Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo);

    Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<SucursalHorarioBloqueo> sucursalHorarioBloqueos);

    Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueoAsync(SucursalHorarioBloqueo sucursalHorarioBloqueo);

    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalHorarioAsync(short idSucursalHorario);

    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalAsync(byte idSucursal);

    Task<SucursalHorarioBloqueo?> FindByIdAsync(long id);

    Task<bool> ExistsSucursalHorarioBloqueoAsync(long id);
}
