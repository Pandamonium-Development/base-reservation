using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReserva(BaseReservationContext context) : IRepositoryReserva
{
    /// <summary>
    /// Create reservation
    /// </summary>
    /// <param name="reserva">Reservation model to be added</param>
    /// <returns>Reserva</returns>
    public async Task<Reserva> CreateReservaAsync(Reserva reserva)
    {
        var result = context.Reservas.Add(reserva);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Update reservation
    /// </summary>
    /// <param name="reserva">Reservation model to be updated</param>
    /// <returns>Reserva</returns>
    public async Task<Reserva> UpdateReservaAsync(Reserva reserva)
    {
        context.Reservas.Update(reserva);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(reserva.Id);
        return response!;
    }

    /// <summary>
    /// Get reservation with specific id
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>Reserva if founded, otherwise null</returns>
    public async Task<Reserva?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reserva))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Reserva>()
            .Include(a => a.DetalleReservas)
            .ThenInclude(a => a.IdServicioNavigation)
            .ThenInclude(a => a.IdTipoServicioNavigation)
            .Include(a => a.IdSucursalNavigation)
            .Include(a => a.ReservaPregunta)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id);
    }

    /// <summary>
    /// Validate if exists reservation
    /// </summary>
    /// <param name="id">Id to look for</param>
    /// <returns>True if exists, if not, false</returns>
    public async Task<bool> ExistsReservaAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reserva))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Reserva>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id) != null;
    }

    /// <summary>
    /// Get list of all reservations by branch and date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="dia">Date to look for</param>
    /// <returns>ICollection of Reserva</returns>
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly dia)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal && m.Fecha == dia)
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all reservations
    /// </summary>
    /// <returns>ICollection of Reserva</returns>
    public async Task<ICollection<Reserva>> ListAllAsync()
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all reservations by branch
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <returns>ICollection of Reserva</returns>
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal)
            .ToListAsync();
        return collection;
    }

    /// <summary>
    /// Get list of all reservations by branch, date start and end date
    /// </summary>
    /// <param name="idSucursal">Branch id</param>
    /// <param name="fechaInicio">Start date</param>
    /// <param name="fechaFin">En date</param>
    /// <returns>ICollection of Reserva</returns>
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
            .ToListAsync();
        return collection;
    }
}