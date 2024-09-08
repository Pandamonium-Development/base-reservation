using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceDetalleReserva(IRepositoryDetalleReserva repository, IMapper mapper,
                                    IValidator<DetalleReserva> reservaServicioValidator) : IServiceDetalleReserva
{
    /// <inheritdoc />
    public async Task<bool> CreateDetalleReservaAsync(int idReserva, IEnumerable<RequestDetalleReservaDto> detallesReserva)
    {
        var servicios = await ValidateDetalleReservaAsync(idReserva, detallesReserva);

        var result = await repository.CreateDetalleReservaAsync(idReserva, servicios);
        if (!result) throw new ListNotAddedException("Error al guardar servicios.");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseDetalleReservaDto?> FindByIdAsync(int id)
    {
        var reservaServicio = await repository.FindByIdAsync(id);
        if (reservaServicio == null) throw new NotFoundException("Servicio en la reserva no encontrado.");

        return mapper.Map<ResponseDetalleReservaDto>(reservaServicio);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseDetalleReservaDto>> ListAllByReservaAsync(int idReserva)
    {
        var list = await repository.ListAllByReservaAsync(idReserva);
        var collection = mapper.Map<ICollection<ResponseDetalleReservaDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate reservation details to be added
    /// </summary>
    /// <param name="idReserva">Branch id</param>
    /// <param name="detallesReserva">List of reservation details to be validated</param>
    /// <returns>IEnumerable of DetalleReserva</returns>
    private async Task<IEnumerable<DetalleReserva>> ValidateDetalleReservaAsync(int idReserva, IEnumerable<RequestDetalleReservaDto> detallesReserva)
    {
        var serviciosExistentes = mapper.Map<List<DetalleReserva>>(detallesReserva);
        foreach (var item in serviciosExistentes)
        {
            item.IdReserva = idReserva;
            await reservaServicioValidator.ValidateAndThrowAsync(item);
        }
        return serviciosExistentes;
    }
}