using AutoMapper;
using BaseReservation.Infrastructure;
using BaseReservation.Domain.Exceptions;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Domain.Core.Specifications;
using BaseReservation.Application.Core.Interfaces;
using BaseReservation.Application.Services.Interfaces;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceReservationQuestion(ICoreService<ReservationQuestion> coreService, IMapper mapper) : IServiceReservationQuestion
{
    /// <inheritdoc />
    public async Task<ResponseReservationQuestionDto> FindByIdAsync(long id)
    {
        if (!await coreService.UnitOfWork.Repository<ReservationQuestion>().ExistsAsync(id)) throw new NotFoundException("Pregunta no encontrada.");

        var spec = new BaseSpecification<ReservationQuestion>(x => x.Id == id);
        var reservationQuestion = await coreService.UnitOfWork.Repository<ReservationQuestion>().FirstOrDefaultAsync(spec);

        return mapper.Map<ResponseReservationQuestionDto>(reservationQuestion);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservationQuestionDto>> ListAllAsync()
    {
        var reservationQuestions = await coreService.UnitOfWork.Repository<ReservationQuestion>().ListAllAsync();
        return mapper.Map<ICollection<ResponseReservationQuestionDto>>(reservationQuestions);
    }
}