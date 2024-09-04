using BaseReservation.Infrastructure.Models;

namespace BaseReservation.Infrastructure.Repository.Interfaces;

public interface IRepositoryReserva
{
    Task<Reserva> CreateReservaAsync(Reserva reserva);

    Task<Reserva> UpdateReservaAsync(Reserva reserva);

    Task<ICollection<Reserva>> ListAllAsync();

    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal);

    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin);

    Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly dia);

    Task<Reserva?> FindByIdAsync(int id);

    Task<bool> ExistsReserva(int id);

}
