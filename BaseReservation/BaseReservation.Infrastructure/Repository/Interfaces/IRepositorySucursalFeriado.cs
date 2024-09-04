
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalFeriado
{
    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal);

    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin);

    Task<ICollection<SucursalFeriado>> ListAllBySucursalAsync(byte idSucursal, short anno);

    Task<SucursalFeriado?> FindByIdAsync(short id);

    Task<bool> CreateSucursalFeriadosAsync(byte idSucursal, IEnumerable<SucursalFeriado> sucursalFeriados);
}
