using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceReservationDetail(IRepositoryReservationDetail repository, IMapper mapper,
                                    IValidator<ReservationDetail> detalleReservaValidator) : IServiceReservationDetail
{
    /// <inheritdoc />
    public async Task<bool> CreateReservationDetailAsync(int reservationId, IEnumerable<RequestReservationDetailDto> reservationDetails)
    {
        var validatedReservationDetails = await ValidateReservationDetailAsync(reservationId, reservationDetails);

        var result = await repository.CreateReservationDetailAsync(reservationId, validatedReservationDetails);
        if (!result) throw new ListNotAddedException("Error al guardar detalles de reserva.");

        return result;
    }

    /// <inheritdoc />
    public async Task<ResponseReservationDetailDto?> FindByIdAsync(int id)
    {
        var reservationDetail = await repository.FindByIdAsync(id);
        if (reservationDetail == null) throw new NotFoundException("Detalle de reserva no encontrado.");

        return mapper.Map<ResponseReservationDetailDto>(reservationDetail);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationDetailDto>> ListAllByReservationAsync(int reservationId)
    {
        var list = await repository.ListAllByReservationAsync(reservationId);
        var collection = mapper.Map<ICollection<ResponseReservationDetailDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validate reservation details to be added
    /// </summary>
    /// <param name="reservationId">Branch id</param>
    /// <param name="reservationDetails">List of reservation details to be validated</param>
    /// <returns>IEnumerable of ReservationDetail</returns>
    private async Task<IEnumerable<ReservationDetail>> ValidateReservationDetailAsync(int reservationId, IEnumerable<RequestReservationDetailDto> reservationDetails)
    {
        var mappedReservationDetails = mapper.Map<List<ReservationDetail>>(reservationDetails);
        foreach (var item in mappedReservationDetails)
        {
            item.ReservationId = reservationId;
            await detalleReservaValidator.ValidateAndThrowAsync(item);
        }
        return mappedReservationDetails;
    }
}