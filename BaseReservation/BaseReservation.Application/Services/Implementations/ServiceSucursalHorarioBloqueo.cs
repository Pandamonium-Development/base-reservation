using BaseReservation.Application.Comunes;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using AutoMapper;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceSucursalHorarioBloqueo(IRepositorySucursalHorarioBloqueo repository,
                                                 IValidator<SucursalHorarioBloqueo> bloqueoValidator, IMapper mapper) : IServiceSucursalHorarioBloqueo
{
    public async Task<ResponseSucursalHorarioBloqueoDto> CreateSucursalHorarioBloqueoAsync(RequestSucursalHorarioBloqueoDto bloqueoDTO)
    {
        var bloqueo = await ValidateSucursalHorarioBloqueo(bloqueoDTO);

        var result = await repository.CreateSucursalHorarioBloqueoAsync(bloqueo);
        if (result == null) throw new NotFoundException("Horario bloqueo no se ha creado.");

        return mapper.Map<ResponseSucursalHorarioBloqueoDto>(result);
    }

    public async Task<bool> CreateSucursalHorarioBloqueoAsync(short idSucursalHorario, IEnumerable<RequestSucursalHorarioBloqueoDto> bloqueos)
    {
        var bloqueosGuardar = await ValidateSucursalHorarioBloqueo(idSucursalHorario, bloqueos);

        var result = await repository.CreateSucursalHorarioBloqueoAsync(idSucursalHorario, bloqueosGuardar);
        if (!result) throw new ListNotAddedException("Error al guardar bloqueos");

        return result;
    }

    public async Task<ResponseSucursalHorarioBloqueoDto> FindByIdAsync(long id)
    {
        var bloqueo = await repository.FindByIdAsync(id);
        if (bloqueo == null) throw new NotFoundException("Horario bloqueo no encontrado.");

        return mapper.Map<ResponseSucursalHorarioBloqueoDto>(bloqueo);
    }

    public async Task<ICollection<ResponseSucursalHorarioBloqueoDto>> ListAllBySucursalHorarioAsync(short idSucursalHorario)
    {
        var bloqueos = await repository.ListAllBySucursalHorarioAsync(idSucursalHorario);

        return mapper.Map<ICollection<ResponseSucursalHorarioBloqueoDto>>(bloqueos);
    }

    public async Task<ResponseSucursalHorarioBloqueoDto> UpdateSucursalHorarioBloqueoAsync(long id, RequestSucursalHorarioBloqueoDto bloqueoDTO)
    {
        if (!await repository.ExistsSucursalHorarioBloqueoAsync(id)) throw new NotFoundException("Horario bloqueo no encontrada.");

        var bloqueo = await ValidateSucursalHorarioBloqueo(bloqueoDTO);
        bloqueo.Id = id;
        var result = await repository.UpdateSucursalHorarioBloqueoAsync(bloqueo);

        return mapper.Map<ResponseSucursalHorarioBloqueoDto>(result);
    }

    private async Task<SucursalHorarioBloqueo> ValidateSucursalHorarioBloqueo(RequestSucursalHorarioBloqueoDto bloqueolDTO)
    {
        var bloqueo = mapper.Map<SucursalHorarioBloqueo>(bloqueolDTO);
        await bloqueoValidator.ValidateAndThrowAsync(bloqueo);
        return bloqueo;
    }

    private async Task<IEnumerable<SucursalHorarioBloqueo>> ValidateSucursalHorarioBloqueo(short idSucursalHorario, IEnumerable<RequestSucursalHorarioBloqueoDto> bloqueoDtos)
    {
        var bloqueos = mapper.Map<List<SucursalHorarioBloqueo>>(bloqueoDtos);
        foreach (var item in bloqueos)
        {
            item.Id = 0;
            item.IdSucursalHorario = idSucursalHorario;
            await bloqueoValidator.ValidateAndThrowAsync(item);
        }
        return bloqueos;
    }
}