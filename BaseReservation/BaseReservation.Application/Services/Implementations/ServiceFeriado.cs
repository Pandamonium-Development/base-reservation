using AutoMapper;
using BaseReservation.Application.Comunes;
using BaseReservation.Application.RequestDTOs;
using BaseReservation.Application.ResponseDTOs;
using BaseReservation.Application.Services.Interfaces;
using BaseReservation.Infrastructure.Models;
using BaseReservation.Infrastructure.Repository.Interfaces;
using FluentValidation;

namespace BaseReservation.Application.Services.Implementations;

public class ServiceFeriado(IRepositoryFeriado repository, IMapper mapper,
                            IValidator<Feriado> feriadoValidator) : IServiceFeriado
{
    /// <inheritdoc />
    public async Task<ResponseFeriadoDto> CreateFeriadoAsync(RequestFeriadoDto feriadoDto)
    {
        var feriado = await ValidarFeriado(feriadoDto);

        var result = await repository.CreateFeriadoAsync(feriado);
        if (result == null) throw new NotFoundException("Feriado no creado.");

        return mapper.Map<ResponseFeriadoDto>(result);
    }

    /// <inheritdoc />
    public async Task<ResponseFeriadoDto> UpdateFeriadoAsync(byte id, RequestFeriadoDto feriadoDto)
    {
        if (!await repository.ExistsFeriadoAsync(id)) throw new NotFoundException("Feriado no encontrada.");

        var producto = await ValidarFeriado(feriadoDto);
        producto.Id = id;
        var result = await repository.UpdateFeriadoAsync(producto);

        return mapper.Map<ResponseFeriadoDto>(result);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteFeriadoAsync(byte id)
    {
        if (!await repository.ExistsFeriadoAsync(id)) throw new NotFoundException("Feriado no encontrada.");
        return await repository.DeleteFeriadoAsync(id);
    }

    /// <inheritdoc />
    public async Task<ResponseFeriadoDto> FindByIdAsync(byte id)
    {
        var feriado = await repository.FindByIdAsync(id);
        if (feriado == null) throw new NotFoundException("Feriado no encontrado.");

        return mapper.Map<ResponseFeriadoDto>(feriado);
    }

    /// <inheritdoc />
    public async Task<ICollection<ResponseFeriadoDto>> ListAllAsync()
    {
        var list = await repository.ListAllAsync();
        var collection = mapper.Map<ICollection<ResponseFeriadoDto>>(list);

        return collection;
    }

    /// <summary>
    /// Validates the `RequestFeriadoDto` object by mapping it to a `Feriado` object 
    /// and applying the corresponding validation rules.
    /// </summary>
    /// <param name="feriadoDTO">The `RequestFeriadoDto` object containing the holiday data to validate</param>
    /// <returns>RequestFeriadoDto</returns>
    private async Task<Feriado> ValidarFeriado(RequestFeriadoDto feriadoDTO)
    {
        var feriado = mapper.Map<Feriado>(feriadoDTO);
        await feriadoValidator.ValidateAndThrowAsync(feriado);
        return feriado;
    }
}