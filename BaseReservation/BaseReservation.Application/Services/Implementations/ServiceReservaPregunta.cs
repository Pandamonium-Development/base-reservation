using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceReservaPregunta(IRepositoryReservaPregunta repository, IMapper mapper) : IServiceReservaPregunta
{
    /// <inheritdoc />
    public async Task<ResponseReservaPreguntaDto> FindByIdAsync(int id)
    {
        var reserva = await repository.FindByIdAsync(id);
        if (reserva == null) throw new NotFoundException("Pregunta no encontrada.");

        return mapper.Map<ResponseReservaPreguntaDto>(reserva);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseReservaPreguntaDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseReservaPreguntaDto>>(list);

        return collection;
    }
}