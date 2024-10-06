using BaseReservation.Application.Common;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceReservationQuestion(IRepositoryReservationQuestion repository, IMapper mapper) : IServiceReservationQuestion
{
    /// <inheritdoc />
    public async Task<ResponseReservationQuestionDto> FindByIdAsync(int id)
    {
        var reservationQuestion = await repository.FindByIdAsync(id);
        if (reservationQuestion == null) throw new NotFoundException("Pregunta no encontrada.");

        return mapper.Map<ResponseReservationQuestionDto>(reservationQuestion);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationQuestionDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseReservationQuestionDto>>(list);

        return collection;
    }
}