using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalHorarioBloqueo
{
    Task<SucursalHorarioBloqueo> CreateSucursalHorarioBloqueolAsync(SucursalHorarioBloqueo bloqueo);

    Task<bool> CreateSucursalHorarioBloqueolAsync(short idSucursalHorario, IEnumerable<SucursalHorarioBloqueo> bloqueo);

    Task<SucursalHorarioBloqueo> UpdateSucursalHorarioBloqueolAsync(SucursalHorarioBloqueo bloqueo);

    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalHorarioAsync(short idSucursalHorario);

    Task<ICollection<SucursalHorarioBloqueo>> ListAllBySucursalAsync(byte idSucursal);

    Task<SucursalHorarioBloqueo?> FindByIdAsync(long id);

    Task<bool> ExistsHorarioBloqueoAsync(long id);
}
