using BaseReservation.Infrastructure.Data;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BaseReservation.Infrastructure.Repository.Implementations;

public class RepositoryReserva(BaseReservationContext context) : IRepositoryReserva
{
    /// <inheritdoc />
    public async Task<Reserva> CreateReservaAsync(Reserva reserva)
    {
        var result = context.Reservas.Add(reserva);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc />
    public async Task<Reserva> UpdateReservaAsync(Reserva reserva)
    {
        context.Reservas.Update(reserva);

        await context.SaveChangesAsync();

        var response = await FindByIdAsync(reserva.Id);
        return response!;
    }

    /// <inheritdoc />
    public async Task<Reserva?> FindByIdAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reserva))!.FindPrimaryKey()!.Properties[0];
        return await context.Set<Reserva>()
            .Include(a => a.DetalleReservas)
            .ThenInclude(a => a.IdServicioNavigation!)/*TO DO*/
            .ThenInclude(a => a.IdTipoServicioNavigation)
            .Include(a => a.IdSucursalNavigation)
            .Include(a => a.ReservaPregunta)
            .AsNoTracking()
        .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id);
    }

    /// <inheritdoc />
    public async Task<bool> ExistsReservaAsync(int id)
    {
        var keyProperty = context.Model.FindEntityType(typeof(Reserva))!.FindPrimaryKey()!.Properties[0];

        return await context.Set<Reserva>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => EF.Property<int>(a, keyProperty.Name) == id) != null;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reserva>> ListAllAsync()
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly dia)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal && m.Fecha == dia)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal)
            .ToListAsync();
        return collection;
    }

    /// <inheritdoc />
    public async Task<ICollection<Reserva>> ListAllBySucursalAsync(byte idSucursal, DateOnly fechaInicio, DateOnly fechaFin)
    {
        var collection = await context.Set<Reserva>()
            .AsNoTracking()
            .Where(m => m.IdSucursal == idSucursal && m.Fecha >= fechaInicio && m.Fecha <= fechaFin)
            .ToListAsync();
        return collection;
    }
}