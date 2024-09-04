using BaseReservation.Infrastructure.Enums;
using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositorySucursalHorario
{
    Task<ICollection<SucursalHorario>> ListAllBySucursalAsync(byte idSucursal);

    Task<SucursalHorario?> FindByIdAsync(short id);

    Task<SucursalHorario?> FindByDiaAsync(byte idSucursal, DiaSemana dia);

    Task<bool> CreateSucursalHorariosAsync(byte idSucursal, IEnumerable<SucursalHorario> sucursalHorarios);
}